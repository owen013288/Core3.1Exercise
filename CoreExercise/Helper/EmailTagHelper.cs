using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CoreExercise.Helper
{
    // using Microsoft.AspNetCore.Razor.TagHelpers; => TagHelper
    public class EmailTagHelper : TagHelper
    {
        public const string DomainName = "codemagic.com.tw";
        public string MailTo { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            //輸出element Tag名稱 <a></a>
            output.TagName = "a";

            //設定Email Adress
            var mailAddress = $"{MailTo}@{DomainName}";

            //設定href屬性
            output.Attributes.SetAttribute("href", $"mailto:{mailAddress}");

            //設定element內容文字
            output.Content.SetContent(mailAddress);
        }
    }
}
