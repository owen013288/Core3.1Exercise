using CoreExercise.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;

namespace CoreExercise.Controllers
{
    public class FirstController : Controller
    {
        // using Microsoft.Extensions.Logging; => ILogger
        private readonly ILogger<FirstController> _logger;

        // 依賴注入 -- 控制反轉
        public FirstController(ILogger<FirstController> logger)
        {
            _logger = logger;

            _logger.LogInformation("this is a logger info data");
        }

        public IActionResult Index()
        {
            this._logger.LogInformation("12345678");

            #region ViewData
            base.ViewData["User1"] = new CurrentUser()
            {
                Id = 1,
                Name = "ViewData-Name",
                Account = "ViewData-Account",
                Email = "ViewData-Email",
                Password = "ViewData-Password",
                LoginTime = DateTime.Now
            };
            ViewData["Something"] = "ViewData-Something";
            #endregion

            #region ViewBag
            base.ViewBag.Name = "ViewBag.Name";
            base.ViewBag.Description = "ViewBag.Description";
            base.ViewBag.User = new CurrentUser()
            {
                Id = 2,
                Name = "ViewBag.User-Name",
                Account = "ViewBag.User-Account",
                Email = "ViewBag.User-Email",
                Password = "ViewBag.User-Password",
                LoginTime = DateTime.Now
            };
            ViewBag.Something = "ViewBag.Something";
            #endregion

            #region TempData
            base.TempData["User"] = new CurrentUser()
            {
                Id = 3,
                Name = "TempData[\"User\"]-Name",
                Account = "TempData[\"User\"]-Account",
                Email = "TempData[\"User\"]-Email",
                Password = "TempData[\"User\"]-Password",
                LoginTime = DateTime.Now
            };
            #endregion

            #region Session: 服務器內存的一段內容，在HttpContext
            if (string.IsNullOrWhiteSpace(this.HttpContext.Session.GetString("CurrentUserSession")))
            {
                // using Microsoft.AspNetCore.Http; => SetString
                HttpContext.Session.SetString("CurrentUserSession",
                    // using System.Text.Json; => JsonSerializer
                    JsonSerializer.Serialize(new CurrentUser()
                    {
                        Id = 4,
                        Name = "HttpContext.Session.SetString(\"CurrentUserSession\",JsonSerializer.Serialize(new CurrentUser(){Name = XXX}",
                        Account = "HttpContext.Session.SetString(\"CurrentUserSession\",JsonSerializer.Serialize(new CurrentUser(){Account = XXX}",
                        Email = "HttpContext.Session.SetString(\"CurrentUserSession\",JsonSerializer.Serialize(new CurrentUser(){Email = XXX}",
                        Password = "HttpContext.Session.SetString(\"CurrentUserSession\",JsonSerializer.Serialize(new CurrentUser(){Password = XXX}",
                        LoginTime = DateTime.Now
                    }));
            }
            #endregion

            #region Model
            return View(new CurrentUser()
            {
                Id = 5,
                Name = "Model-Name",
                Account = "Model-Account",
                Email = "Model-Email",
                Password = "Model-Password",
                LoginTime = DateTime.Now
            });
            #endregion
        }

        /// <summary>
        /// 轉向測試
        /// </summary>
        /// <returns></returns>
        public IActionResult RedirectToOtherAction()
        {
            // ViewData、ViewBag 不能用在轉向
            ViewData["Test1"] = "test1";
            ViewBag.Test2 = "test2";
            // TempData 可以用在轉向，但轉完後重新整理則消失
            TempData["Test3"] = "test3";
            
            return RedirectToAction("Index", "Friends");
        }
    }
}
