using CoreExercise.IService;
using Microsoft.AspNetCore.Mvc;

namespace CoreExercise.Controllers
{
    public class DIController : Controller
    {
        private readonly IBankService _bankService;

        public DIController(IBankService bankService)
        {
            _bankService = bankService;
        }

        public IActionResult Balance()
        {
            return View(_bankService);
        }
    }
}
