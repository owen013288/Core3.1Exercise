using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.IO;

namespace CoreExercise
{
    public class Program
    {
        // using System.Collections.Generic; => Dictionary
        // 建立Dictionary<TKey,TValue> 型別集合，且Key中必須包含索引值，才能載入組態檔
        public static Dictionary<string, string> DictEmployees { get; } = new Dictionary<string, string>
        {
                {"Asia:employees:1", "Mary"},
                {"Asia:employees:2", "John"},
                {"Asia:employees:3", "Kevin"},
                {"Asia:employees:4", "David"},
                {"Asia:employees:5", "Rose"}
        };

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

                // 將Dictionary<TKey,TValue>集合加入組態
                config.AddInMemoryCollection(DictEmployees);

                // 載入自訂的 JSON 組態檔
                config.AddJsonFile("Food.json", optional: true, reloadOnChange: true);
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
    }
}
