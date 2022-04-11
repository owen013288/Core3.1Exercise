using CoreExercise.Helper;
using CoreExercise.IService;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Threading.Tasks;

namespace CoreExercise.Controllers
{
    public class AdditionalLearnController : Controller
    {
        private readonly IEmailSender _emailSender;

        public AdditionalLearnController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public IActionResult Index()
        {
            return View();
        }

        // using System.Threading.Tasks; => Task
        [HttpPost]
        public async Task<IActionResult> SendEmail()
        {
            string S_CSS = "<b style='width: 5em;display:inline-block;'>";
            string E_CSS = "</b>";
            // using System.Text; => StringBuilder
            StringBuilder sb_Body = new StringBuilder();
            sb_Body.Append("<div style=\"font-family: 'Microsoft JhengHei', sans-serif;color: #555;font-size: 12pt;\">");
            sb_Body.Append("<caption><h2>使用者建議與勘誤回報信件</h2></caption>");
            sb_Body.Append(S_CSS + "商店名稱：owen" + "<br/>");
            sb_Body.Append(S_CSS + "使用者來源IP：" + HttpContext.Connection.RemoteIpAddress.ToString() + E_CSS + "<br/>");
            sb_Body.Append(S_CSS + "<br/>使用者建議內容：" + E_CSS + "<br/>");
            sb_Body.Append("</div>");

            await _emailSender.SendEmailAsync(
                // using CoreExercise.Helper; => SysConfig
                SysConfig.Cfg.ContactUsEmail,
                null,
                null,
                $"{SysConfig.Cfg.SiteName} 使用者建議與勘誤回報信件",
                sb_Body.ToString()
            );

            TempData["message"] = "謝謝您提供的建議!";
            TempData["dialogclose"] = true;
            return RedirectToAction("Index");
        }
    }
}
