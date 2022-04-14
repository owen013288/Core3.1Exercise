using Microsoft.AspNetCore.Mvc;

namespace CoreExercise.Controllers
{
    public class ViewComponentController : Controller
    {
        public IActionResult GetProductList()
        {
            return View();
        }
    }
}
