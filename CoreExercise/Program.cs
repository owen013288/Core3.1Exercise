using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace CoreExercise
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                // using Microsoft.Extensions.Configuration; => AddJsonFile
                // using System.IO; => Path
                config.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(),
                    "appsettings.json"), true, true);
                // ���ܰ򩳸��|�]�w
                config.SetBasePath(Directory.GetCurrentDirectory() + "/ConfigFiles/");
                // ���J�ۭq�� JSON �պA��
                config.AddJsonFile("FutureCorp.json", optional: true, reloadOnChange: true);
                // ���J�ۭq�� INI �պA��
                config.AddIniFile("Mobile.ini", optional: true, reloadOnChange: true);
                // ���J�ۭq�� XML �պA��
                config.AddXmlFile("Computer.xml", optional: true, reloadOnChange: true);
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
    }
}
