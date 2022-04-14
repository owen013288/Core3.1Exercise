using CoreExercise.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CoreExercise.Controllers
{
    public class ViewComponentController : Controller
    {
        public List<Card> Cards { get; } = new List<Card>
        {
            new Card { Name = "Elon Musk", Brief="特斯拉創辦人 伊隆·馬斯克", Photo="ElonMusk.jpg", WikiUrl="https://goo.gl/46xeXx" },
            new Card { Name = "Mark Zuckerberg", Brief="Facebook創辦人 馬克·祖伯克", Photo="MarkZuckerberg.jpg", WikiUrl="https://goo.gl/BktGGA" },
            new Card { Name = "Steve Jobs", Brief="蘋果創辦人 史提夫·賈伯斯", Photo="SteveJobs.jpg", WikiUrl="https://goo.gl/nAiX0y" },
            new Card { Name = "Vader", Brief="帝國元帥  維達", Photo="Vader.jpg", WikiUrl="https://en.wikipedia.org/wiki/Darth_Vader" },
            new Card { Name = "Darth Mual", Brief="星際大戰 達斯摩", Photo="DarthMual.jpg", WikiUrl="https://goo.gl/5obLhX"},
            new Card { Name = "White Twilek", Brief="星際大戰 女絕地武士Twilek", Photo="WhiteTwilek.jpg", WikiUrl="https://goo.gl/reKzAu" },
            new Card { Name = "White Twilek", Brief="星際大戰 歐比旺Obiwan", Photo="Obiwan.jpg", WikiUrl="http://bit.ly/33gxdgt" },
            new Card { Name = "White Twilek", Brief="德國總理 梅克爾", Photo="Merkel.jpg", WikiUrl="http://bit.ly/33huSlv" },
        };

        public IActionResult GetProductList()
        {
            return View();
        }

        public IActionResult ListHeros()
        {
            return View(Cards);
        }

        public IActionResult ProductListTagHelper()
        {
            return View();
        }
    }
}
