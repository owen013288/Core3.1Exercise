using CoreExercise.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CoreExercise.ViewComponents
{
    // using Microsoft.AspNetCore.Mvc; => ViewComponent
    // 這樣套用就不用名稱加上「ViewComponent」
    [ViewComponent(Name = "HeroList")]
    public class Heros : ViewComponent
    {
        public IViewComponentResult Invoke(List<Card> data)
        {
            return View(data);
        }
    }
}
