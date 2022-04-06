using CoreExercise.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
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
            // 添加Identity
            // using CoreExercise.Models; => ApplicationDbContext
            // using Microsoft.EntityFrameworkCore; => UseSqlServer
            // using Microsoft.AspNetCore.Identity; => IdentityUser
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("ExerciseConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            // 該怎麼用Session
            services.AddSession();

            // using System.Text.Json; => PropertyNamingPolicy
            services.AddControllersWithViews().AddRazorRuntimeCompilation().AddJsonOptions(option =>
            {
                // WriteIndented：把 JSON 格式排版美化，預設 false，通常 Production 版本不會做這個設定。
                option.JsonSerializerOptions.WriteIndented = true;
                // PropertyNamingPolicy：可自訂序列反序列化的命名規則，可以指定為 JsonNamingPolicy.CamelCase，預設 null
                // PropertyNameCaseInsensitive：預設 false，屬性名稱是否要忽略大小寫，通常設定了 JsonNamingPolicy.CamelCase 後，這個應該不需要特別設定，除非有特殊需求。
                option.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                // IgnoreNullValues：忽略 null 值的屬性，預設 false。
                option.JsonSerializerOptions.IgnoreNullValues = true;

                // 添加Identity
                services.AddRazorPages();
            });
        }

        // using Microsoft.Extensions.Logging; => ILoggerFactory
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            // using NLog.Extensions.Logging;
            loggerFactory.AddNLog();

            // 表示請求需要使用Session
            app.UseSession();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // 添加Identity
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                // 添加Identity
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            // 添加Identity
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            // 端口路由定義
            app.UseRouting();

            // 添加Identity
            app.UseAuthentication();
            app.UseAuthorization();

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
