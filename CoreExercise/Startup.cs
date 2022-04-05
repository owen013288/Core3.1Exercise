using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
            // �ӫ���Session
            services.AddSession();

            // using System.Text.Json; => PropertyNamingPolicy
            services.AddControllersWithViews().AddRazorRuntimeCompilation().AddJsonOptions(option =>
            {
                // WriteIndented�G�� JSON �榡�ƪ����ơA�w�] false�A�q�` Production �������|���o�ӳ]�w�C
                option.JsonSerializerOptions.WriteIndented = true;
                // PropertyNamingPolicy�G�i�ۭq�ǦC�ϧǦC�ƪ��R�W�W�h�A�i�H���w�� JsonNamingPolicy.CamelCase�A�w�] null
                // PropertyNameCaseInsensitive�G�w�] false�A�ݩʦW�٬O�_�n�����j�p�g�A�q�`�]�w�F JsonNamingPolicy.CamelCase ��A�o�����Ӥ��ݭn�S�O�]�w�A���D���S��ݨD�C
                option.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                // IgnoreNullValues�G���� null �Ȫ��ݩʡA�w�] false�C
                option.JsonSerializerOptions.IgnoreNullValues = true;
            }); ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // ��ܽШD�ݭn�ϥ�Session
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

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
