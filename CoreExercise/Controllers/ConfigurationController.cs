using CoreExercise.Options;
using CoreExercise.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CoreExercise.Controllers
{
    public class ConfigurationController : Controller
    {
        // Configuration相依性注入設定
        // using Microsoft.Extensions.Configuration; => IConfiguration
        private readonly IConfiguration _config;

        // using CoreExercise.Options; => FoodOptions
        private readonly FoodOptions _foodOptions;

        // using CoreExercise.Options; => IOptionsMonitor
        public ConfigurationController(IConfiguration config, IOptionsMonitor<FoodOptions> foodOptions)
        {
            _config = config;

            // Option使用前,必須在DI Container中註冊
            // 利用Options Pattern從Configuration組態檔中讀入
            _foodOptions = foodOptions.CurrentValue;
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

            // 傳統作法，找不到就回傳null
            ViewData["CPU_SPARC"] = _config["cpu:abcdef"];

            // 用GetValue<T>方法讀取組態值
            // 新做法，找不到就傳回預設值
            ViewData["CPU_ARM"] = _config.GetValue<string>("cpu:abcdef", "找不到指定資料");
            return View();
        }

        // 在View直接注入IConfiguration相依性物件
        public IActionResult InjectConfigView()
        {
            return View();
        }

        // 用GetSection()方法取得組態的sub-section
        public IActionResult GetSection()
        {
            var corp = _config.GetSection("FutureCorp");
            var website = _config.GetSection("FutureCorp:Website");
            var branches = _config.GetSection("FutureCorp:Branches");
            var usa = _config.GetSection("FutureCorp:Branches:USA");
            var usa_name = _config.GetSection("FutureCorp:Branches:USA:Name");

            var corp_children = corp.GetChildren();
            var website_children = website.GetChildren();
            var branches_children = branches.GetChildren();
            var usa_children = usa.GetChildren();
            var usa_name_children = usa_name.GetChildren();

            var corp_Enumerable = corp.AsEnumerable();
            var website_Enumerable = website.AsEnumerable();
            var branches_Enumerable = branches.AsEnumerable();
            var usa_Enumerable = usa.AsEnumerable();
            var usa_name_Enumerable = usa_name.AsEnumerable();

            return View();
        }

        // 將組態資料繫結至類別
        public IActionResult BindToClass()
        {
            // 1.使用Bind方法繫結
            var deviceVM = new DeviceViewModel();
            _config.GetSection("MobileOptions").Bind(deviceVM);

            // 2.使用Get<T>()方法繫結
            var device = _config.GetSection("MobileOptions").Get<DeviceViewModel>();

            return View("SelectedDevice", deviceVM);
        }

        // 將組態資料繫結至Object Graph
        public IActionResult BindToObjectGraph()
        {
            // 1.使用Bind方法繫結
            var AICorpVM = new AICorpViewModel();
            _config.GetSection("AICorp").Bind(AICorpVM);

            // 2.使用Get<T>()方法繫結
            var aiCorp = _config.GetSection("AICorp").Get<AICorpViewModel>();

            // 將Object Graph物件序列化成JSON字串, 交予前端顯示
            // using Newtonsoft.Json; => JsonConvert
            ViewData["jsonAICorp"] = JsonConvert.SerializeObject(aiCorp);

            return View(aiCorp);
        }

        // 將組態繫結至類別的陣列屬性
        public IActionResult BindToArray()
        {
            var arrayEmps = new EmployeeViewModel();

            //1.使用Bind方法繫結
            _config.GetSection("Asia").Bind(arrayEmps);

            //2.使用Get<T>()方法繫結
            var arrayEmployees = _config.GetSection("Asia").Get<EmployeeViewModel>();

            return View(arrayEmps);
        }

        public IActionResult FoodWithOptions()
        {
            return View(_foodOptions);
        }
    }
}