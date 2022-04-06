using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
            }); ;
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
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            // 端口路由定義
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                // 這裡修改開啟路徑
                endpoints.MapControllerRoute(
                    name: "default",
                    //pattern: "{controller=Home}/{action=Index}/{id?}");
                    pattern: "{controller=Products}/{action=Index}/{id?}");
            });
        }
    }
}
