using CoreExercise.IService;
using CoreExercise.Models;
using CoreExercise.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CoreExercise.Controllers
{
    public class TagHelpersController : Controller
    {
        private readonly ICityService _cityService;
        public TagHelpersController(ICityService cityService)
        {
            _cityService = cityService;
        }

        //C# 6.0 - Auto Property Initializer
        public List<Hero> heros { get; } = new List<Hero>
        {
            new Hero { Name = "Elon Musk", Brief="特斯拉創辦人 伊隆·馬斯克", Photo="ElonMusk.jpg", WikiUrl="https://goo.gl/46xeXx" },
            new Hero { Name = "Mark Zuckerberg", Brief="Facebook創辦人 馬克·祖伯克", Photo="MarkZuckerberg.jpg", WikiUrl="https://goo.gl/BktGGA" },
            new Hero { Name = "Steve Jobs", Brief="蘋果創辦人 史提夫·賈伯斯", Photo="SteveJobs.jpg", WikiUrl="https://goo.gl/nAiX0y" },
            new Hero { Name = "Vader", Brief="帝國元帥  維達", Photo="Vader.jpg", WikiUrl="https://en.wikipedia.org/wiki/Darth_Vader" },
            new Hero { Name = "Darth Mual", Brief="星際大戰 達斯摩", Photo="DarthMual.jpg", WikiUrl="https://goo.gl/5obLhX"},
            new Hero { Name = "White Twilek", Brief="星際大戰 女絕地武士Twilek", Photo="WhiteTwilek.jpg", WikiUrl="https://goo.gl/reKzAu" },
            new Hero { Name = "Obiwan", Brief="星際大戰 歐比旺Obiwan", Photo="Obiwan.jpg", WikiUrl="http://bit.ly/33gxdgt" },
            new Hero { Name = "Merkel", Brief="德國總理 梅克爾", Photo="Merkel.jpg", WikiUrl="http://bit.ly/33huSlv" }
        };

        public IActionResult PartialTagHelper()
        {
            return View(heros);
        }

        public IActionResult SelectTagHelper()
        {
            var model = new CountryViewModel();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SelectTagHelper(CountryViewModel countryVM)
        {
            if (ModelState.IsValid)
            {
                //讀取國家代碼
                string countryCode = countryVM.Country;

                //由國家代碼查詢名稱
                string country = countryVM.Countries.Where(c => c.Value == countryCode).Select(x => x.Text).FirstOrDefault();

                return RedirectToAction("DisplayCountry", new { Country = country });
            }

            return View(countryVM);
        }

        //顯示Country資訊
        public IActionResult DisplayCountry(string country)
        {
            if (string.IsNullOrEmpty(country))
            {
                return Content("必須提供Country參數!");
            }

            ViewData["Country"] = country;

            return View();
        }

        public IActionResult SelectEnum()
        {
            var model = new CountryEnumViewModel();

            //以下是設定列舉預設值
            //model.EnumerateCountry = CountryEnum.France;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SelectEnum(int EnumerateCountry)
        {
            if (ModelState.IsValid)
            {
                //顯示Country名稱
                return RedirectToAction("DisplayCountry", new { Country = (CountryEnum)EnumerateCountry });
            }

            return View();
        }

        public IActionResult SelectOptionGroup()
        {
            //使用此功能, 必須先初始化CountryGroupViewModel模型類別
            var model = new CountryGroupViewModel();

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        //按<select asp-for="Country" >的設定,接收參數應為string Country即可
        //但為何設定成CountryGroupViewModel型別物件? 因為其Countries會含有六個國家的List<SelectListItem>
        //它可用作LINQ將國家代碼轉換成國家全名, 而不必再額外初化CountryGroupViewModel物件來取得國家資訊
        public IActionResult SelectOptionGroup(CountryGroupViewModel countryVM)
        {
            if (ModelState.IsValid)
            {
                //將國家代碼轉換成國家全名
                var country = countryVM.Countries.Where(c => c.Value == countryVM.Country).Select(x => x.Text).FirstOrDefault();

                //顯示Country名稱
                return RedirectToAction("DisplayCountry", new { Country = country });
            }

            return View();
        }

        public IActionResult CacheTagHelper()
        {
            return View();
        }

        public IActionResult CustomTag()
        {
            return View();
        }
    }
}
