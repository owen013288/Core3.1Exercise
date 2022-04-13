using CoreExercise.Helper;
using CoreExercise.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CoreExercise.Controllers
{
    public class JsonDataApiController : Controller
    {
        private readonly IUtility _utility;
        public JsonDataApiController(IUtility utility)
        {
            _utility = utility;
        }

        // 1.回傳BMW & BENZ汽車銷售數字JSON
        public ActionResult<IEnumerable<CarSales>> GetCarSalesNumber()
        {
            List<CarSales> CarSalesNumber = new List<CarSales>
            {
                new CarSales { Id = 1, Car = "BMW", Salesdata = new int[] { 120, 200, 300, 350, 400, 250, 380, 330, 500, 280, 310, 330 } },
                new CarSales { Id = 2,  Car = "BENZ", Salesdata = new int[] { 220, 150, 350, 300, 300, 200, 180, 400, 420, 210, 250, 440 }},
            };

            return CarSalesNumber;
        }

        // 2.以亂數產生1-12月Audi & Lexus汽車銷售數據
        public ActionResult<IEnumerable<CarSales>> GetCarSalesNumberRandom()
        {
            // 以亂數產生1-12月數據
            var random1 = _utility.GetNumbers(12);
            var random2 = _utility.GetNumbers(12);

            List<CarSales> CarSalesNumber = new List<CarSales>
            {
                new CarSales { Id = 1438, Car = "Audi", Salesdata = random1 },
                new CarSales { Id = 9563, Car = "Lexus", Salesdata = random2 },
            };

            return CarSalesNumber;
        }

        // 3.回傳地區月均溫JSON資料
        public ActionResult<IEnumerable<Location>> GetTemperature()
        {
            // List集合包含台北,台中及高雄三個地方的氣溫資料
            List<Location> Locations = new List<Location>
            {
                new Location {
                    City="臺北",
                    Temperature = new double[] { 16.1, 16.5, 18.5, 21.9, 25.2, 27.7, 29.6, 29.2, 27.4, 24.5, 21.5, 17.9, 23 }
                },
                new Location {
                    City="臺中",
                    Temperature = new double[] { 16.6, 17.3, 19.6, 23.1, 26.0, 27.6, 28.6, 28.3, 27.4, 25.2, 21.9, 18.1, 23.3 }
                },
                new Location {
                    City="高雄",
                    Temperature = new double[] { 19.3, 20.3, 22.6, 25.4, 27.5, 28.5, 29.2, 28.7, 28.1, 26.7, 24.0, 20.6, 25.1 }
                }
            };

            return Locations;
        }
    }
}
