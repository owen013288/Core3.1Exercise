using Microsoft.AspNetCore.Mvc;

namespace CoreExercise.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
