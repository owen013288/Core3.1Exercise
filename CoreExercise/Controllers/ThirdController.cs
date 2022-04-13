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

        public IActionResult JsonChart()
        {
            string[] Labels = { "1月", "2月", "3月", "4月", "5月", "6月", "7月", "8月", "9月", "10月", "11月", "12月" };

            // 1.序列化成為JSON物件結構字串
            string jsonLabels = System.Text.Json.JsonSerializer.Serialize(Labels);

            // 以ViewData將資料傳給View
            ViewData["JsonLabels"] = jsonLabels;

            // 2.List集合包含台北,台中及高雄三個地方的氣溫資料
            List<Location> Locations = new List<Location>
            {
                new Location{
                    City="台北",
                    Temperature = new double[] { 16.1, 16.5, 18.5, 21.9, 25.2, 27.7, 29.6, 29.2, 27.4, 24.5, 21.5, 17.9 }
                },
                new Location{
                    City="台中",
                    Temperature = new double[] { 16.6, 17.3, 19.6, 23.1, 26.0, 27.6, 28.6, 28.3, 27.4, 25.2, 21.9, 18.1 }
                },
                new Location{
                    City="高雄",
                    Temperature = new double[] { 19.3, 20.3, 22.6, 25.4, 27.5, 28.5, 29.2, 28.7, 28.1, 26.7, 24.0, 20.6 }
                }
            };

            //將List集合序列化成為JSON物件結構字串
            string JsonLocations = System.Text.Json.JsonSerializer.Serialize(Locations);
            //以ViewData將資料傳給View
            ViewData["JsonLocations"] = JsonLocations;

            return View(Locations);
        }

        public IActionResult CarSalesAjaxJSON()
        {
            return View();
        }
    }
}
