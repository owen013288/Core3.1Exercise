using CoreExercise.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CoreExercise.Controllers
{
    public class ThirdController : Controller
    {
        // using System.Collections.Generic; => List
        // using CoreExercise.Models; => Person
        private List<Person> persons { get; }
        public ThirdController()
        {
            persons = new List<Person> {
                new Person { Id=1, Name="Kevin", Email="kevin@gmail.com" },
                new Person { Id=2, Name="David", Email="kevin@gmail.com" },
                new Person { Id=3, Name="Rose", Email="kevin@gmail.com" }
            };
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult JsonIndex()
        {
            return View(persons);
        }
    }
}
