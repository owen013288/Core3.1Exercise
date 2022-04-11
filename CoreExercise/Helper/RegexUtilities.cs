using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace CoreExercise.Helper
{
    /// <summary>
    /// 正規表達式共用工具
    /// </summary>
    public class RegexUtilities
    {
        /// <summary>
        /// 是否符合Email格式
        /// </summary>
        /// <param name="email">Email內容</param>
        /// <returns></returns>
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // 規範化域
                // using System.Text.RegularExpressions; => Regex
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                    RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // 檢查電子郵件的域部分並將其規範化。
                static string DomainMapper(Match match)
                {
                    // 使用 IdnMapping 類轉換 Unicode 域名。
                    // using System.Globalization; => IdnMapping
                    var idn = new IdnMapping();

                    // 提取並處理域名（無效時拋出 ArgumentException）
                    var domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}
