using NLog;

namespace CoreExercise.Helper
{
    /// <summary>
    /// Log助手
    /// </summary>
    public static class LoggerHelper
	{
		public readonly static ILogger logger = LogManager.GetLogger("LoggerHelper");

		/// <summary>
		/// 調試
		/// </summary>
		/// <param name="message">訊息</param>
		/// <param name="args">其他參數</param>
		public static void LogDebug(string message, params object[] args)
		{
			logger.Debug(message, args);
		}

		/// <summary>
		/// 錯誤
		/// </summary>
		/// <param name="message">訊息</param>
		/// <param name="args">其他參數</param>
		public static void LogError(string message, params object[] args)
		{
			logger.Error(message, args);
		}

		/// <summary>
		/// 資訊
		/// </summary>
		/// <param name="message">訊息</param>
		/// <param name="args">其他參數</param>
		public static void LogInformation(string message, params object[] args)
		{
			logger.Info(message, args);
		}

		/// <summary>
		/// 警告
		/// </summary>
		/// <param name="message">訊息</param>
		/// <param name="args">其他參數</param>
		public static void LogWarning(string message, params object[] args)
		{
			logger.Warn(message, args);
		}
	}
}
