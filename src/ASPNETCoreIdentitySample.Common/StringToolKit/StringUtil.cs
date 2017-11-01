using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace ASPNETCoreIdentitySample.Common.StringToolKit
{
    public static class StringUtil
    {
        public static long ConvertToLong(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return 0;
            else
                return Convert.ToInt64(str.Replace(",", ""));
        }
        public static string GeneratetrackId(int length)
        {
            StringBuilder myGuidLikeString = new StringBuilder();
            while (myGuidLikeString.Length < length)
            {
                myGuidLikeString.Append(Guid.NewGuid().ToString().Replace("-", ""));
            }
            return myGuidLikeString.ToString(0, length);
        }

        #region Split String And Join Array List

        /// <summary>
        /// Get the extension from the given filename
        /// </summary>
        /// <param name="fileName">the given filename ie:abc.123.txt</param>
        /// <returns>the extension ie:txt</returns>
        public static string GetFileExtension(this string fileName)
        {
            string ext = string.Empty;
            int fileExtPos = fileName.LastIndexOf(".", StringComparison.Ordinal);
            if (fileExtPos >= 0)
                ext = fileName.Substring(fileExtPos, fileName.Length - fileExtPos);

            return ext;
        }

        public static string GetFileName(this string fileName)
        {
            string ext = string.Empty;
            int fileExtPos = fileName.LastIndexOf(".", StringComparison.Ordinal);
            if (fileExtPos >= 0)
                ext = fileName.Substring(0, fileExtPos);

            return ext;
        }

        public static Guid[] ConvertStringArrayToGuidArray(char splitChar, string valuelist)
        {
            return valuelist.Split(splitChar).Select(Guid.Parse).ToArray();
        }

        public static Guid ConvertStringToGuid(string strGuid)
        {
            var newGuid = Guid.Parse(strGuid);
            return newGuid;
        }

        public static Guid[] ConvertStringsToGuids(string[] strGuids)
        {
            return !strGuids.Any() ? null : strGuids.Select(Guid.Parse).ToArray();
        }

        public static string[] SplitString(char splitChar, string valuelist)
        {
            return valuelist.Split(new char[] { splitChar });
        }

        public static string[] SplitString(string valuelist)
        {
            char[] delimiterChars = { ' ', ',', '.', ':', '\t', '\n', '،', '|' };
            return valuelist.Split(delimiterChars);
        }

        public static string[] SplitStringEmails(string valuelist)
        {
            char[] delimiterChars = { ' ', ',', ':', '\t', '\n', '،', '|' };
            return valuelist.Split(delimiterChars);
        }


        public static string JoinSplitString(string splitChar, ArrayList sections)
        {
            var selected = (string[])sections.ToArray(typeof(string));
            return string.Join(splitChar, selected);
        }

        public static string JoinSplitString(string splitChar, string[] sections)
        {
            return string.Join(splitChar, sections);
        }

        public static string JoinSplitString(string splitChar, Guid[] sections)
        {
            return string.Join(splitChar, sections);
        }

        public static string[] ConvertArrayListToStrings(ArrayList sections)
        {
            return (string[])sections.ToArray(typeof(string));
        }

        #endregion

        #region اندازه متن رشته ای
        /// <summary>
        /// اندازه متن رشته ای
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int StringLength(string str)
        {
            var strLen = str.Length;
            return strLen;
        }
        #endregion

        #region  برش بخشی از متن - Substring
        /// <summary>
        /// برش بخشی از متن - Substring
        /// </summary>
        /// <param name="inputText"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string SubStringText(string inputText, int startIndex, int length)
        {
            var strText = inputText;

            if (strText.Length > length)
            {
                return strText.Substring(startIndex, length) + " ... ";
            }
            else
            {
                return strText;
            }
        }
        #endregion

        public static bool IsValidPhone(string s)
        {
            if (string.IsNullOrEmpty(s)) return false;
            return (!string.IsNullOrEmpty(GetRightMobileNumber(s)));
        }

        public static string GetRightMobileNumber(string phone)
        {
            if (phone == null) return null;

            var number = phone.Replace("+", "")
                .Replace(" ", "")
                .Replace("(", "")
                .Replace(")", "")
                .Replace("-", "")
                .Replace("۰", "0")
                .Replace("۱", "1")
                .Replace("۲", "2")
                .Replace("۳", "3")
                .Replace("۴", "4")
                .Replace("۵", "5")
                .Replace("۶", "6")
                .Replace("۷", "7")
                .Replace("۸", "8")
                .Replace("۹", "9")
                .TrimStart('0');

            if (number.Length < 10) return null;

            number = number.Substring(number.Length - 10, 10);
            if (number.Substring(0, 1) != "9") return null;

            try
            {
                Int64.Parse(number);
            }
            catch (Exception)
            {
                return null;
            }
            return "0" + number;
        }
    }
}
