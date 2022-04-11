using CoreExercise.Extension;
using CoreExercise.Serivce;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CoreExercise.Helper
{
    /// <summary>
    /// SysConfig 系統參數設定調整
    /// </summary>
    public class SysConfig
    {
        /// <summary>
        /// 配置文件
        /// </summary>
        private static SysConfig _Cfg;

        /// <summary>
        /// 預設密件副本收件者
        /// </summary>
        public List<string> MailBcc { get; set; } = new List<string>();

        /// <summary>
        /// 聯絡我們 Email 設定
        /// </summary>
        public List<string> ContactUsEmail { get; set; } = new List<string>();

        /// <summary>
        /// 系統名稱
        /// </summary>
        public string SiteName { get; set; } = "系統名稱尚未設定";

        /// <summary>
        /// 聯絡電話
        /// </summary>
        public string ContactPhone { get; set; }

        /// <summary>
        /// 諮詢電話
        /// </summary>
        public string SupportPhone { get; set; }

        /// <summary>
        /// 系統更新日期
        /// </summary>
        public DateTime UpdateDt { get; set; } = DateTime.Now;

        /// <summary>
        /// 寄件者
        /// </summary>
        public string Sender => StmpServer.Sender;

        /// <summary>
        /// 配置文件路徑
        /// </summary>
        internal static string ConfgFilePath
        {
            get
            {
                // using System.IO; => Path
                return Path.Combine(Directory.GetCurrentDirectory(), 
                    "SysConfig.Development.json");
            }
        }

        /// <summary>
        /// 配置文件
        /// </summary>
        public static SysConfig Cfg
        {
            get
            {
                if (_Cfg != null)
                    return _Cfg;

                if (!File.Exists(ConfgFilePath))
                    new SysConfig().Save(out _);

                try
                {
                    // using Newtonsoft.Json; => JsonConvert
                    _Cfg = JsonConvert.DeserializeObject<SysConfig>(File.ReadAllText(ConfgFilePath));
                }
                catch
                {
                    _Cfg = new SysConfig()
                    {
                        MailBcc = new List<string>(),
                        ContactUsEmail = new List<string>(),
                        // using CoreExercise.Serivce; => EmailSettings
                        StmpServer = new EmailSettings()
                        {
                            MailServer = "127.0.0.1",
                            MailPort = 25,
                            SenderName = string.Empty,
                            Sender = string.Empty,
                            Password = string.Empty
                        }
                    };
                }
                return _Cfg;
            }
        }

        /// <summary>
        /// 郵件發信主機設定
        /// </summary>
        public EmailSettings StmpServer { get; set; } = new EmailSettings()
        {
            MailServer = "127.0.0.1",
            MailPort = 25,
            SenderName = string.Empty,
            Sender = string.Empty,
            Password = string.Empty
        };

        /// <summary>
        /// 儲存
        /// </summary>
        /// <param name="Msg">訊息</param>
        /// <returns></returns>
        public bool Save(out List<string> Msg)
        {
            Msg = new List<string>();
            if (string.IsNullOrWhiteSpace(SiteName))
                Msg.Add("系統名稱設定錯誤");
            if (MailBcc.Any(x => !RegexUtilities.IsValidEmail(x)))
                Msg.Add("預設密件副本收件者錯誤");
            if (ContactUsEmail.Any(x => !RegexUtilities.IsValidEmail(x)))
                Msg.Add("聯絡我們 Email 設定錯誤");
            if (!RegexUtilities.IsValidEmail(StmpServer.Sender))
                Msg.Add("預設寄件者 Email 設定錯誤");
            if (Msg.Any())
                return false;

            // using CoreExercise.Extension; => ToSafehtml
            SiteName = this.SiteName.ToSafehtml();

            try
            {
                File.WriteAllText(ConfgFilePath, JsonConvert.SerializeObject(this, Formatting.Indented));
            }
            catch (Exception ex)
            {
                LoggerHelper.LogError(ex.ToString());
                Msg.Add("寫入 JSON 設定檔過程發生異常!!");
                return false;
            }

            _Cfg = null;
            return true;
        }
    }
}
