using Microsoft.AspNetCore.Mvc;

namespace CoreExercise.Controllers
{
    public class ThirdController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
