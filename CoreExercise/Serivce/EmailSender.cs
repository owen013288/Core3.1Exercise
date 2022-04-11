using CoreExercise.Extension;
using CoreExercise.IService;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreExercise.Serivce
{
    /// <summary>
    /// 寄信實作
    /// </summary>
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _emailSettings;

        // using Microsoft.Extensions.Options; => IOptions
        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        /// <summary>
        /// 重新加載Config
        /// </summary>
        public void ReloadConfig()
        {
            _emailSettings.Reload();
        }

        /// <summary>
        /// 寄信
        /// </summary>
        /// <param name="ToList">聯絡我們的設定</param>
        /// <param name="CcList">副本</param>
        /// <param name="BccList">密件副本</param>
        /// <param name="subject">主旨</param>
        /// <param name="message">內容</param>
        /// <returns></returns>
        public async Task SendEmailAsync(IEnumerable<string> ToList, IEnumerable<string> CcList, IEnumerable<string> BccList, string subject, string message)
        {
            var HasTo = ToList?.Any() ?? false;
            var HasCc = CcList?.Any() ?? false;
            var HasBcc = BccList?.Any() ?? false;

            if (!HasTo && !HasCc && !HasBcc) return;

            // using MimeKit; => MimeMessage
            var mimeMessage = new MimeMessage();

            mimeMessage.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.Sender));

            if (HasTo) mimeMessage.To.AddRange(ToList.Select(x => new MailboxAddress(x)));

            if (HasCc) mimeMessage.Cc.AddRange(CcList.Select(x => new MailboxAddress(x)));

            if (HasBcc) mimeMessage.Bcc.AddRange(BccList.Select(x => new MailboxAddress(x)));

            mimeMessage.Subject = subject;

            mimeMessage.Body = new TextPart("html")
            {
                // using CoreExercise.Extension; => ToSafehtml
                Text = message.ToSafehtml()
            };

            await SendEmailAsync(mimeMessage);
        }

        /// <summary>
        /// 寄信
        /// </summary>
        /// <param name="mimeMessage">MimeMessage主體</param>
        /// <returns></returns>
        public async Task SendEmailAsync(MimeMessage mimeMessage)
        {
            // using MailKit.Net.Smtp; => SmtpClient
            using (var client = new SmtpClient())
            {
                var _SecureSocketOptions = _emailSettings.UseSSL ? 
                    MailKit.Security.SecureSocketOptions.Auto : 
                    MailKit.Security.SecureSocketOptions.None;

                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                await client.ConnectAsync(_emailSettings.MailServer, _emailSettings.MailPort, _SecureSocketOptions);

                if (!String.IsNullOrWhiteSpace(_emailSettings.Sender) && !String.IsNullOrWhiteSpace(_emailSettings.Password))
                {
                    await client.AuthenticateAsync(_emailSettings.Sender, _emailSettings.Password);
                }

                await client.SendAsync(mimeMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
