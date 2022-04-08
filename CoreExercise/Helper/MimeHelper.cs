using Microsoft.AspNetCore.Http;
using MimeDetective;
using System.IO;
using System.Linq;

namespace CoreExercise.Helper
{
	/// <summary>
	/// Mime助手
	/// </summary>
	public static class MimeHelper
	{
		// using MimeDetective; => ContentInspector
		private static readonly ContentInspector Inspector = new ContentInspectorBuilder()
		{
			Definitions = MimeDetective.Definitions.Default.All()
		}.Build();

		/// <summary>
		/// 取得檔案格式
		/// </summary>
		/// <param name="Input">byte[]資料</param>
		/// <param name="ResetPosition">重定向</param>
		/// <returns></returns>
		public static ResultFileType GetFileType(this byte[] Input, bool ResetPosition = true)
		{
			// using System.IO; => Stream
			Stream stream = new MemoryStream(Input);
			return GetFileType(stream, ResetPosition);
		}

		/// <summary>
		/// 取得檔案格式
		/// </summary>
		/// <param name="Input">Stream資料</param>
		/// <param name="ResetPosition">重定向</param>
		/// <returns></returns>
		public static ResultFileType GetFileType(this Stream Input, bool ResetPosition = true)
		{
			var Content = ContentReader.Default.ReadFromStream(Input, ResetPosition);
			var Results = Inspector.Inspect(Content);
			if (!Results.Any()) return null;
			return new ResultFileType()
			{
				Extension = Results.ByFileExtension().First().Extension,
				Mime = Results.ByMimeType().First().MimeType
			};
		}

		/// <summary>
		/// 最後的檔案格式
		/// </summary>
		public class ResultFileType
		{
			/// <summary>
			/// 副檔名
			/// </summary>
			public string Extension { get; internal set; }

			/// <summary>
			/// Mime格式
			/// </summary>
			public string Mime { get; internal set; }
		}

		/// <summary>
		/// 是否符合上傳格式
		/// </summary>
		/// <param name="file">上傳檔案</param>
		/// <param name="uploadExt">上傳格式</param>
		/// <returns></returns>
		// using Microsoft.AspNetCore.Http; => IFormFile
		public static bool IsConformExt(IFormFile file, params string[] uploadExt)
		{
			string uploadType = MimeHelper.GetFileType(file.OpenReadStream()).Mime;
			return uploadExt.Any(x => x == uploadType);
		}
	}
}
