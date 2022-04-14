using CoreExercise.IService;
using CoreExercise.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace CoreExercise.Services
{
    /// <summary>
    /// 郵遞區號實作
    /// </summary>
    public class TaiwanZipcodeService : IZipcodeService
    {
        public List<ZipcodeViewModel> Cities { get; set; }
        public TaiwanZipcodeService()
        {
            Cities = new List<ZipcodeViewModel>
            {
                new ZipcodeViewModel {
                    CityId = "01",
                    CityName = "基隆市",
                    Districts = new List<District>
                    {
                        new District { Id ="01", Name="仁愛區", Zipcode ="200" },
                        new District { Id ="02", Name="信義區", Zipcode ="201" },
                        new District { Id ="03", Name="中正區", Zipcode ="202" },
                        new District { Id ="04", Name="中山區", Zipcode ="203" },
                        new District { Id ="05", Name="安樂區", Zipcode ="204" },
                        new District { Id ="06", Name="暖暖區", Zipcode ="205" },
                        new District { Id ="07", Name="七堵區", Zipcode ="206" }
                    }
                },
                new ZipcodeViewModel {
                    CityId = "02",
                    CityName = "台北市",
                    Districts = new List<District>
                    {
                        new District { Id ="01", Name="中正區", Zipcode ="100" },
                        new District { Id ="02", Name="大同區", Zipcode ="103" },
                        new District { Id ="03", Name="中山區", Zipcode ="104" },
                        new District { Id ="04", Name="松山區", Zipcode ="105" },
                        new District { Id ="05", Name="大安區", Zipcode ="106" },
                        new District { Id ="06", Name="萬華區", Zipcode ="108" },
                        new District { Id ="07", Name="信義區", Zipcode ="110" },
                        new District { Id ="08", Name="士林區", Zipcode ="111" },
                        new District { Id ="09", Name="北投區", Zipcode ="112" },
                        new District { Id ="10", Name="內湖區", Zipcode ="114" },
                        new District { Id ="11", Name="南港區", Zipcode ="115" },
                        new District { Id ="12", Name="文山區", Zipcode ="116" }
                    }
                },
                new ZipcodeViewModel {
                    CityId = "03",
                    CityName = "新北市",
                    Districts = new List<District>
                    {
                        new District { Id ="01", Name="萬里區", Zipcode ="207" },
                        new District { Id ="02", Name="金山區", Zipcode ="208" },
                        new District { Id ="03", Name="板橋區", Zipcode ="220" },
                        new District { Id ="04", Name="汐止區", Zipcode ="221" },
                        new District { Id ="05", Name="深坑區", Zipcode ="222" },
                        new District { Id ="06", Name="石碇區", Zipcode ="223" },
                        new District { Id ="07", Name="瑞芳區", Zipcode ="224" },
                        new District { Id ="08", Name="平溪區", Zipcode ="226" },
                        new District { Id ="09", Name="雙溪區", Zipcode ="227" },
                        new District { Id ="10", Name="貢寮區", Zipcode ="228" },
                        new District { Id ="11", Name="新店區", Zipcode ="231" },
                        new District { Id ="12", Name="坪林區", Zipcode ="232" },
                        new District { Id ="13", Name="烏來區", Zipcode ="233" },
                        new District { Id ="14", Name="永和區", Zipcode ="234" },
                        new District { Id ="15", Name="中和區", Zipcode ="235" },
                        new District { Id ="16", Name="土城區", Zipcode ="236" },
                        new District { Id ="17", Name="三峽區", Zipcode ="237" },
                        new District { Id ="18", Name="樹林區", Zipcode ="238" },
                        new District { Id ="19", Name="鶯歌區", Zipcode ="239" },
                        new District { Id ="20", Name="三重區", Zipcode ="241" },
                        new District { Id ="21", Name="新莊區", Zipcode ="242" },
                        new District { Id ="22", Name="泰山區", Zipcode ="243" },
                        new District { Id ="23", Name="林口區", Zipcode ="244" },
                        new District { Id ="24", Name="蘆洲區", Zipcode ="247" },
                        new District { Id ="25", Name="五股區", Zipcode ="248" },
                        new District { Id ="26", Name="八里區", Zipcode ="249" },
                        new District { Id ="27", Name="淡水區", Zipcode ="251" },
                        new District { Id ="28", Name="三芝區", Zipcode ="252" },
                        new District { Id ="29", Name="石門區", Zipcode ="253" }
                    }
                },
                new ZipcodeViewModel {
                    CityId = "04",
                    CityName = "桃園市",
                    Districts = new List<District>
                    {
                        new District { Id ="01", Name="中壢區", Zipcode="320" },
                        new District { Id ="02", Name="平鎮區", Zipcode="324" },
                        new District { Id ="03", Name="龍潭區", Zipcode="325" },
                        new District { Id ="04", Name="楊梅區", Zipcode="326" },
                        new District { Id ="05", Name="新屋區", Zipcode="327" },
                        new District { Id ="06", Name="觀音區", Zipcode="328" },
                        new District { Id ="07", Name="桃園區", Zipcode="330" },
                        new District { Id ="08", Name="龜山區", Zipcode="333" },
                        new District { Id ="09", Name="八德區", Zipcode="334" },
                        new District { Id ="10", Name="大溪區", Zipcode="335" },
                        new District { Id ="11", Name="復興區", Zipcode="336" },
                        new District { Id ="12", Name="大園區", Zipcode="337" },
                        new District { Id ="13", Name="蘆竹區", Zipcode="338" }
                    }
                },
            };

            //將List<ZipcodeViewModel>集合轉成JSON文字格式
            //string json = Newtonsoft.Json.JsonConvert.SerializeObject(Cities);
        }

        /// <summary>
        /// 查詢郵遞區號
        /// </summary>
        /// <param name="cityName">縣市名</param>
        /// <param name="districtName">行政區名</param>
        /// <returns></returns>
        public string QueryZipcode(string cityName, string districtName)
        {
            if (string.IsNullOrEmpty(cityName) && string.IsNullOrEmpty(districtName))
                return "請提供縣市名與行政區名";

            // 1.查詢City
            ZipcodeViewModel _city = Cities.Where(c => c.CityName.Contains(cityName)).Select(p => p).FirstOrDefault();

            if (_city == null)
                return "查無此City";

            // 2.查詢District
            District _district = _city.Districts
                                    .Where(d => d.Name.Contains(districtName))
                                    .Select(p=>p).FirstOrDefault();

            if (_district is null)
                return "查無此District";

            // 3.讀取Zipcode
            string _zipcode = _district.Zipcode;

            // 或將三行查詢成合一行(此查詢沒有做防呆,可能產生Null Exception)
            string zipcode = Cities
                                .Where(c => c.CityName.Contains(cityName))
                                .Select(p => p).FirstOrDefault().Districts
                                .Where(d => d.Name.Contains(districtName))
                                .FirstOrDefault().Zipcode;

            return _zipcode;
        }
    }
}
