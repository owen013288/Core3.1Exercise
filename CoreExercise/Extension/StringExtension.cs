using System;

namespace CoreExercise.Extension
{
    public static class StringExtension
    {
        /// <summary>
        /// 移除 html 碼中可能的惡意資訊 , For Html 編輯器輸入資料使用
        /// </summary>
        /// <param name="_S"></param>
        /// <returns></returns>
        public static String ToSafehtml(this String _S)
        {
            if (_S == null)
            {
                return null;
            }
            _S = _S.Trim();
            return HAPSanitizer.SanitizeHtml(_S);
        }
    }
}
