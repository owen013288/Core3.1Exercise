using CoreExercise.ViewModels;
using System.Collections.Generic;

namespace CoreExercise.IService
{
    /// <summary>
    /// 郵遞區號介面
    /// </summary>
    public interface IZipcodeService
    {
        List<ZipcodeViewModel> Cities { get; set; }

        /// <summary>
        /// 查詢郵遞區號
        /// </summary>
        /// <param name="cityName">縣市名</param>
        /// <param name="districtName">行政區名</param>
        /// <returns></returns>
        string QueryZipcode(string cityName, string districtName);
    }
}
