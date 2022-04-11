using Newtonsoft.Json;
using System.IO;

namespace CoreExercise.Serivce
{
    /// <summary>
    /// 信箱設定
    /// </summary>
    public class EmailSettings
    {
        /// <summary>
        /// 寄信MailKit Server
        /// </summary>
        public string MailServer { get; set; }

        /// <summary>
        /// 寄信MailKit server的Port，預設25
        /// </summary>
        public int MailPort { get; set; } = 25;

        /// <summary>
        /// 發件人姓名
        /// </summary>
        public string SenderName { get; set; }

        /// <summary>
        /// 發件人
        /// </summary>
        public string Sender { get; set; }

        /// <summary>
        /// 密碼
        /// </summary>
        public string Password { get; set; }

        public bool UseSSL
        {
            get
            {
                return MailPort != 25;
            }
        }

        private string _CfgFile;

        /// <summary>
        /// 配置文件
        /// </summary>
        /// <param name="configFilePath">配置文件路徑</param>
        public void CfgFile(string configFilePath)
        {
            _CfgFile = configFilePath;
            Reload();
        }

        /// <summary>
        /// 當前配置
        /// </summary>
        class TmpConfig
        {
            public EmailSettings StmpServer { get; set; }
        }

        /// <summary>
        /// 重新加載
        /// </summary>
        public void Reload()
        {
            // using System.IO; => File
            if (!File.Exists(_CfgFile)) 
                return;

            TmpConfig _Cfg = null;

            try
            {
                // using Newtonsoft.Json; => JsonConvert
                _Cfg = JsonConvert.DeserializeObject<TmpConfig>(File.ReadAllText(_CfgFile));
            }
            catch
            {
                return;
            }

            this.MailServer = _Cfg.StmpServer.MailServer;
            this.MailPort = _Cfg.StmpServer.MailPort;
            this.SenderName = _Cfg.StmpServer.SenderName;
            this.Sender = _Cfg.StmpServer.Sender;
            this.Password = _Cfg.StmpServer.Password;
        }
    }
}
