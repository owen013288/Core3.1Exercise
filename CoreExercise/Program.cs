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
                // 改變基底路徑設定
                config.SetBasePath(Directory.GetCurrentDirectory() + "/ConfigFiles/");
                // 載入自訂的 JSON 組態檔
                config.AddJsonFile("FutureCorp.json", optional: true, reloadOnChange: true);
                // 載入自訂的 INI 組態檔
                config.AddIniFile("Mobile.ini", optional: true, reloadOnChange: true);
                // 載入自訂的 XML 組態檔
                config.AddXmlFile("Computer.xml", optional: true, reloadOnChange: true);

                // 載入自訂的 JSON 組態檔
                config.AddJsonFile("Device.json", optional: true, reloadOnChange: true);

                // 載入自訂的 JSON 組態檔
                config.AddJsonFile("AICorp.json", optional: true, reloadOnChange: true);
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
    }
}
