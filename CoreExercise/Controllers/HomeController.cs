using CoreExercise.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace CoreExercise.Controllers
{
    // using Microsoft.AspNetCore.Authorization; => Authorize
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // 允許匿名存取
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            // 利用Identity.Name 判斷是否為特定使用者
            if (User.Identity.Name != "Owen013288@gmail.com")
            {
                return Content($"{User.Identity.Name}無權存取此Action動作方法!");
            }

            return View();
        }

        // 授權給特定角色
        [Authorize(Roles = "Admin, Supervisor")]
        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
