using CoreExercise.Helper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;

namespace CoreExercise.Controllers
{
    public class SecondController : Controller
    {
        // using Microsoft.AspNetCore.Hosting; => IWebHostEnvironment
        private readonly IWebHostEnvironment _env;

        public SecondController(IWebHostEnvironment env)
        {
            _env = env;
        }

        protected string WebRootPath { get { return _env.WebRootPath; } }

        public IActionResult BootstrapIndex()
        {
            return View();
        }

        public IActionResult Index()
        {
            // 顯示環境名稱
            ViewData["EnvironmentName"] = _env.EnvironmentName;
            ViewData["WebRootPath"] = _env.WebRootPath;
            ViewData["WebRootFileProvider"] = _env.WebRootFileProvider;
            ViewData["ContentRootPath"] = _env.ContentRootPath;
            ViewData["ContentRootFileProvider"] = _env.ContentRootFileProvider;
            ViewData["ApplicationName"] = _env.ApplicationName;
            ViewData["pro_WebRootPath"] = WebRootPath;

            return View();
        }

        /// <summary>
        /// 上傳PDF文件
        /// </summary>
        /// <param name="uploadFile">上傳檔案</param>
        /// <returns></returns>
        // using Microsoft.AspNetCore.Http; => IFormFile
        public IActionResult UploadFile(IFormFile uploadFile)
        {
            if (uploadFile != null)
            {
                if (!MimeHelper.IsConformExt(uploadFile, "application/pdf"))
                    return new JsonResult(new { success = false, response = "請上傳PDF檔案。" });

                string path = Path.Combine(WebRootPath, "pdf");

                // 不存在就新增資料夾
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                // using System.IO; => Path
                string filePath = Path.Combine(path, uploadFile.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    uploadFile.CopyTo(fileStream);
                }

                return new JsonResult(new { success = true, response = "上傳成功。" });
            }

            return new JsonResult(new { success = false, response = "上傳失敗。" });
        }

        /// <summary>
        /// 刪除PDF文件
        /// </summary>
        /// <param name="fileName">檔案名稱</param>
        /// <returns></returns>
        public IActionResult DeleteFile(string fileName)
        {
            var path = "";
            if (fileName != null)
                path = Path.Combine(WebRootPath, "pdf", fileName);

            if (path != "")
            {
                System.IO.File.Delete(path);
                return new JsonResult(new { success = true, response = "刪除成功。" });
            }

            return new JsonResult(new { success = false, response = "刪除失敗。" });
        }

        /// <summary>
        /// 取得PDF文件
        /// </summary>
        /// <returns></returns>
        public IActionResult GetFile()
        {
            var path = Path.Combine(WebRootPath, "pdf");

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            // using System.Collections.Generic; => List
            List<string> list = new List<string>();
            DirectoryInfo di = new DirectoryInfo(path);
            foreach (var fi in di.GetFiles())
            {
                list.Add(fi.Name);
            }

            return Json(list);
        }
    }
}
