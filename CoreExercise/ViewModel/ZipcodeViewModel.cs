using System.Collections.Generic;

namespace CoreExercise.ViewModels
{
    /// <summary>
    /// 郵遞區號ViewModel
    /// </summary>
    public class ZipcodeViewModel
    {
        public string CityId { get; set; }
        public string CityName { get; set; }
        public List<District> Districts { get; set; }
    }

    /// <summary>
    /// 行政區
    /// </summary>
    public class District
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Zipcode { get; set; }
    }
}
