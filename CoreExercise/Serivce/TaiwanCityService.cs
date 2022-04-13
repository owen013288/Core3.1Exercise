using CoreExercise.IService;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace CoreExercise.Serivce
{
    public class TaiwanCityService : ICityService
    {
        public List<SelectListItem> GetAllCities()
        {
            List<SelectListItem> Cities = new List<SelectListItem>
            {
                new SelectListItem { Text = "基隆市", Value="01" },
                new SelectListItem { Text = "臺北市", Value="02" },
                new SelectListItem { Text = "新北市", Value="03" },
                new SelectListItem { Text = "桃園市", Value="04" },
                new SelectListItem { Text = "新竹市", Value="05" },
                new SelectListItem { Text = "新竹縣", Value="06" },
                new SelectListItem { Text = "苗栗縣", Value="07" },
                new SelectListItem { Text = "臺中市", Value="08" },
                new SelectListItem { Text = "彰化縣", Value="09" },
                new SelectListItem { Text = "南投縣", Value="10" },
                new SelectListItem { Text = "雲林縣", Value="11" },
                new SelectListItem { Text = "嘉義市", Value="12" },
                new SelectListItem { Text = "嘉義縣", Value="13" },
                new SelectListItem { Text = "臺南市", Value="14" },
                new SelectListItem { Text = "高雄市", Value="15" },
                new SelectListItem { Text = "屏東縣", Value="16" },
                new SelectListItem { Text = "臺東縣", Value="17" },
                new SelectListItem { Text = "花蓮縣", Value="18" },
                new SelectListItem { Text = "宜蘭縣", Value="19" },
                new SelectListItem { Text = "澎湖縣", Value="20" },
                new SelectListItem { Text = "金門縣", Value="21" },
                new SelectListItem { Text = "連江縣", Value="22" }
            };

            return Cities;
        }
    }
}
