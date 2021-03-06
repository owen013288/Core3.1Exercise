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

        /// <summary>
        /// 透過Action使用相依性注入
        /// </summary>
        /// <param name="myBankService"></param>
        /// <returns></returns>
        public IActionResult InjectAction([FromServices] IBankService myBankService)
        {
            return View("Balance", myBankService);
        }

        /// <summary>
        /// 透過View使用相依性注入
        /// </summary>
        /// <param name="myBankService"></param>
        /// <returns></returns>
        public IActionResult InjectView()
        {
            return View();
        }

        /// <summary>
        /// 透過View使用相依性注入-台灣區域範例
        /// </summary>
        /// <param name="myBankService"></param>
        /// <returns></returns>
        public IActionResult InjectZipCodeService()
        {
            return View();
        }

        /// <summary>
        /// 使用相依性注入Config
        /// </summary>
        /// <returns></returns>
        public IActionResult InjectConfig()
        {
            return View();
        }
    }
}
