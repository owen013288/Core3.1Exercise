using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CoreExercise.Controllers
{
    public class ConfigurationController : Controller
    {
        // Configuration相依性注入設定
        // using Microsoft.Extensions.Configuration; => IConfiguration
        private readonly IConfiguration _config;
        public ConfigurationController(IConfiguration config)
        {
            _config = config;
        }

        public IActionResult Index()
        {
            // 在Controller讀取Configuration
            ViewData["Website"] = _config["Company:Website"];
            ViewData["Taipei_Name"] = _config["Company:Branches:Taipei:Name"];
            ViewData["Taipei_Tel"] = _config["Company:Branches:Taipei:Tel"];

            // 讀取FutureCorp.json組態檔的設定值
            ViewData["Website1"] = _config["FutureCorp:Website"];
            ViewData["USA_Name"] = _config["FutureCorp:Branches:USA:Name"];
            ViewData["USA_Tel"] = _config["FutureCorp:Branches:USA:Tel"];

            //讀取Mobile.ini組態檔的設定值
            ViewData["CPU_Name"] = _config["CPU:Name"];
            ViewData["CPU_Designer"] = _config["CPU:Designer"];
            ViewData["CPU_Manufacturer"] = _config["CPU:Manufacturer"];
            ViewData["iPhone11Pro_storage"] = _config["Spec:iPhone11Pro:storage"];

            //讀取Computer.xml組態檔的設定值
            ViewData["CPU_Intel"] = _config["cpu:intel"];
            ViewData["CPU_AMD"] = _config["cpu:amd"];
            ViewData["DRAM_Kingston"] = _config["dram:kingston"];
            ViewData["SSD_Samsung"] = _config["ssd:samsung"];
            return View();
        }

        // 在View直接注入IConfiguration相依性物件
        public IActionResult InjectConfigView()
        {
            return View();
        }
    }
}
