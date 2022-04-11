using MimeKit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreExercise.IService
{
    /// <summary>
    /// 寄信介面
    /// </summary>
    public interface IEmailSender
    {
        // using System.Threading.Tasks; => Task
        // using System.Collections.Generic; => IEnumerable
        /// <summary>
        /// 寄信
        /// </summary>
        /// <param name="ToList">聯絡我們的設定</param>
        /// <param name="CcList">副本</param>
        /// <param name="BccList">密件副本</param>
        /// <param name="subject">主旨</param>
        /// <param name="message">內容</param>
        /// <returns></returns>
        Task SendEmailAsync(IEnumerable<string> ToList, IEnumerable<string> CcList, IEnumerable<string> BccList, string subject, string message);

        // using MimeKit; => MimeMessage
        /// <summary>
        /// 寄信
        /// </summary>
        /// <param name="mimeMessage">MimeMessage主體</param>
        /// <returns></returns>
        Task SendEmailAsync(MimeMessage mimeMessage);

        /// <summary>
        /// 重新加載Config
        /// </summary>
        void ReloadConfig();
    }
}
