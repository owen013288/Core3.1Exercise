using CoreExercise.Helper;
using CoreExercise.IService;
using CoreExercise.Models;
using CoreExercise.Options;
using CoreExercise.Serivce;
using CoreExercise.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System;
using System.Text.Json;

namespace CoreExercise
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Identity
            // 添加Identity
            // using CoreExercise.Models; => ApplicationDbContext
            // using Microsoft.EntityFrameworkCore; => UseSqlServer
            // using Microsoft.AspNetCore.Identity; => IdentityUser
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("ExerciseConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            // 添加Identity
            services.AddRazorPages();

            // 配置Identity
            services.Configure<IdentityOptions>(options =>
            {
                // 密碼設置
                // 密碼要有數字
                options.Password.RequireDigit = true;
                // 不一定要有小寫英文字母
                options.Password.RequireLowercase = false;
                // 不需要符號字元
                options.Password.RequireNonAlphanumeric = false;
                // 不需要有大寫英文字母
                options.Password.RequireUppercase = false;
                // 密碼至少要6個字元長
                options.Password.RequiredLength = 6;
                // 至少要有個字元不一樣
                options.Password.RequiredUniqueChars = 6;

                // 鎖定配置
                // using System; => TimeSpan
                // 5分鐘沒有動靜就自動鎖住定網站，預設5分鐘
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                // 三次密碼誤就鎖定網站, 預設5次
                options.Lockout.MaxFailedAccessAttempts = 3;
                // 新增的使用者也會被鎖定，就是犯規沒有新人優待
                options.Lockout.AllowedForNewUsers = true;

                // 使用者配置
                // 取得或設定用來驗證使用者名稱的使用者名稱中允許的字元清單。
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                // 郵箱不能重覆使用
                options.User.RequireUniqueEmail = true;
            });

            // Seting the Account Login page 
            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                // 開啟避免XSS攻擊讀取Cookie
                options.Cookie.HttpOnly = true;
                // 控制 cookie 要多少時間才能從其建立點起保持有效。 過期資訊位於受保護的 cookie 票券中。
                // 因此，即使在瀏覽器應該加以清除之後會傳送至伺服器，會忽略過期的 cookie。
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });
            #endregion

            #region 基本配置
            // 該怎麼用Session
            services.AddSession();

            // using System.Text.Json; => PropertyNamingPolicy
            services.AddControllersWithViews().AddRazorRuntimeCompilation()
                // 將TempData Provider 由 Cookie 改成 Session
                .AddSessionStateTempDataProvider()
                .AddJsonOptions(option =>
            {
                // WriteIndented：把 JSON 格式排版美化，預設 false，通常 Production 版本不會做這個設定。
                option.JsonSerializerOptions.WriteIndented = true;
                // PropertyNamingPolicy：可自訂序列反序列化的命名規則，可以指定為 JsonNamingPolicy.CamelCase，預設 null
                // PropertyNameCaseInsensitive：預設 false，屬性名稱是否要忽略大小寫，通常設定了 JsonNamingPolicy.CamelCase 後，這個應該不需要特別設定，除非有特殊需求。
                option.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                // IgnoreNullValues：忽略 null 值的屬性，預設 false。
                option.JsonSerializerOptions.IgnoreNullValues = true;
            });
            #endregion

            #region DI注入
            // 在DI Container 中註冊CustomizeObj類別
            services.Configure<CustomizeObj>(options =>
            // 將組態繫結至 CustomizeObj 類別
            Configuration.GetSection("CustomizeObj").Bind(options));

            services.AddSingleton<IUtility, Utility>();
            services.AddTransient<IBankService, FubonBankService>();
            services.AddTransient<IZipcodeService, TaiwanZipcodeService>();
            services.AddTransient<IDeviceService, ComputerService>();
            #endregion

            #region 寄信配置
            // using CoreExercise.Serivce; => EmailSettings
            services.Configure<EmailSettings>(options =>
            {
                // using CoreExercise.Helper; => SysConfig
                options.CfgFile(SysConfig.ConfgFilePath);
            })
                // using CoreExercise.IService; => IEmailSender
                .AddSingleton<IEmailSender, EmailSender>();
            #endregion

            #region CROS服務
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin(); // 允許任何
                    });
            });
            #endregion

            #region Configure注入
            services.Configure<FoodOptions>(Configuration);
            // services.Configure<FoodOptions>(options => Configuration.GetSection("FoodOptions").Bind(options));

            // 將組態ComputerOptions區段繫結到DeviceOptions類別
            services.Configure<DeviceOptions>(options => Configuration.GetSection("ComputerOptions").Bind(options));
            #endregion
        }

        // using Microsoft.Extensions.Logging; => ILoggerFactory
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            // using NLog.Extensions.Logging;
            loggerFactory.AddNLog();

            if (env.IsDevelopment())
            {
                // 開發者例外頁
                app.UseDeveloperExceptionPage();

                // 添加Identity
                // 資料庫例外錯誤頁
                app.UseDatabaseErrorPage();
            }
            else
            {
                // 一般例外頁
                app.UseExceptionHandler("/Home/Error");

                // 添加Identity
                // HTTP Strict Transport Security Protocol
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //當用戶輸入的網址找不到時↓
            app.UseStatusCodePagesWithRedirects("~/Error/404.html");

            // 將HTTP 轉向 HTTPS
            app.UseHttpsRedirection();
            // 使用純靜態文件支持的中間件，而不使用帶有終端的中間件
            // 啟用靜態檔服務
            app.UseStaticFiles();

            // 使用Cookie Policy
            app.UseCookiePolicy();

            // 端口路由定義
            // 使用路由
            app.UseRouting();

            // 使用CROS仲介軟體 (需介於UseRouting、UseEndpoints之間)
            app.UseCors();

            // 添加Identity
            // UseAuthentication 將驗證 中介軟體 新增至要求管線。
            // 身分驗證
            app.UseAuthentication();

            // 添加驗證中間件
            // 授權驗證
            app.UseAuthorization();

            // 表示請求需要使用Session
            app.UseSession();

            // 端口路由
            app.UseEndpoints(endpoints =>
            {
                // 這裡修改開啟路徑
                endpoints.MapControllerRoute(
                    name: "default",
                    //pattern: "{controller=Home}/{action=Index}/{id?}");
                    pattern: "{controller=Products}/{action=Index}/{id?}");

                // 添加Identity
                endpoints.MapRazorPages();
            });
        }
    }
}
