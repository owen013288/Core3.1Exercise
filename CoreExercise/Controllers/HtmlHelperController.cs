using CoreExercise.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CoreExercise.Controllers
{
    public class HtmlHelperController : Controller
    {
        public IActionResult Index()
        {
            User register = new User
            {
                Id = 1001,
                Name = "奚江華",
                Nickname = "聖殿祭司",
                Email = "kevin@gmail.com",
                City = 2,
                Terms = true
            };

            ViewData.Model = register;

            return View();
        }

        public IActionResult HelpersBootstrap()
        {
            Register register = new Register
            {
                Id = 1,
                Name = "Kevin",
                Email = "kevin@gmail.com"
            };

            return View(register);
        }

        public IActionResult EditorFor()
        {
            RegisterDataAnnotations register = new RegisterDataAnnotations
            {
                Id = 1,
                Name = "聖殿祭司",
                Password = "myPassword",
                Email = "kevin@gmail.com",
                HomePage = "http://blog.sina.com.tw",
                Gender = Gender.Male,
                Birthday = new DateTime(1980, 6, 16),
                Birthday2 = new DateTime(1980, 6, 16),
                City = 4,
                Commutermode = "1",
                Comment = "請留下您的意見",
                Terms = true
            };

            return View(register);
        }

        [HttpPost]
        public IActionResult EditorFor(RegisterDataAnnotations register)
        {
            if (ModelState.IsValid)
            {
                return Content("成功!");
            }
            else
            {
                ModelState.AddModelError("msg1", "model資料未通過模型驗證");
            }

            return View(register);
        }
    }
}
