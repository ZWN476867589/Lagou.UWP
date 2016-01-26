using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Reflection;
using Lagou.API.Attributes;
using AngleSharp.Parser.Html;

namespace Lagou.API {
    public static class StringHelper {

        private static readonly Regex HtmlClearReg = new Regex(@"<br[\s]*/?>", RegexOptions.IgnoreCase);

        /// <summary>
        /// 從URL中取 Key / Value
        /// </summary>
        /// <param name="s"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static Dictionary<string, string> ParseString(this string s, bool ignoreCase) {
            //必須這樣,請不要修改
            if (string.IsNullOrEmpty(s)) {
                return new Dictionary<string, string>();
            }

            if (s.IndexOf('?') != -1) {
                s = s.Remove(0, s.IndexOf('?'));
            }

            Dictionary<string, string> kvs = new Dictionary<string, string>();
            Regex reg = new Regex(@"[\?&]?(?<key>[^=]+)=(?<value>[^\&]*)");
            MatchCollection ms = reg.Matches(s);
            string key;
            foreach (Match ma in ms) {
                key = ignoreCase ? ma.Groups["key"].Value.ToLower() : ma.Groups["key"].Value;
                if (kvs.ContainsKey(key)) {
                    kvs[key] += "," + ma.Groups["value"].Value;
                } else {
                    kvs[key] = ma.Groups["value"].Value;
                }
            }

            return kvs;
        }

        /// <summary>
        /// 設置 URL中的 key
        /// </summary>
        /// <param name="url"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        public static string SetUrlKeyValue(this string url, string key, string value, Encoding encode = null) {
            if (url == null)
                url = "";
            if (String.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");
            if (value == null)
                value = "";
            if (null == encode)
                encode = Encoding.UTF8;
            //if (!string.IsNullOrEmpty(url.ParseString(key, true).Trim())) {
            if (url.ParseString(true).ContainsKey(key.ToLower())) {
                Regex reg = new Regex(@"([\?\&])(" + key + @"=)([^\&]*)(\&?)", RegexOptions.IgnoreCase);
                //　如果 value 前几个字符是数字，有BUG
                //return reg.Replace(url, "$1$2" + HttpUtility.UrlEncode(value, encode) + "$4");

                return reg.Replace(url, (ma) => {
                    if (ma.Success) {
                        return string.Format("{0}{1}{2}{3}", ma.Groups[1].Value, ma.Groups[2].Value, value, ma.Groups[4].Value);
                    }
                    return "";
                });

            } else {
                return string.Format("{0}{1}{2}={3}",
                    url,
                    (url.IndexOf('?') > -1 ? "&" : "?"),
                    key,
                    value);
                //return url + (url.IndexOf('?') > -1 ? "&" : "?") + key + "=" + value;
            }
        }


        /// <summary>
        /// 修正URL
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string FixUrl(this string url) {
            return url.FixUrl("");
        }

        /// <summary>
        /// 修正URL
        /// </summary>
        /// <param name="url"></param>
        /// <param name="defaultPrefix"></param>
        /// <returns></returns>
        public static string FixUrl(this string url, string defaultPrefix) {
            // 必須這樣,請不要修改
            if (url == null)
                url = "";

            if (defaultPrefix == null)
                defaultPrefix = "";
            string tmp = url.Trim();
            if (!Regex.Match(tmp, "^(http|https):").Success) {
                tmp = string.Format("{0}/{1}", defaultPrefix, tmp);
            }
            tmp = Regex.Replace(tmp, @"(?<!(http|https):)[\\/]+", "/").Trim();
            return tmp;
        }


        #region To int
        /// <summary>
        ///
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int ToInt(this string str, int defaultValue) {
            int v;
            if (int.TryParse(str, out v)) {
                return v;
            } else
                return defaultValue;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int ToInt(this string str) {
            return str.ToInt(0);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int? ToIntOrNull(this string str, int? defaultValue) {
            int v;
            if (int.TryParse(str, out v))
                return v;
            else
                return defaultValue;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int? ToIntOrNull(this string str) {
            return str.ToIntOrNull(null);
        }

        /// <summary>
        /// 智慧轉換為 Int ，取字串中的第一個數位串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int SmartToInt(this string str, int defaultValue) {
            if (string.IsNullOrEmpty(str))
                return defaultValue;

            //Match ma = Regex.Match(str, @"(\d+)");
            Match ma = Regex.Match(str, @"((-\s*)?\d+)");
            if (ma.Success) {
                return ma.Groups[1].Value.Replace(" ", "").ToInt(defaultValue);
            } else {
                return defaultValue;
            }
        }
        #endregion

        public static ErrorTypes ParseErrorType(this string str) {
            var fs = typeof(ErrorTypes).GetRuntimeFields();
            foreach (var f in fs) {
                var attr = f.GetCustomAttribute<ErrorTagAttribute>();
                if (attr != null && str.IndexOf(attr.Tag, StringComparison.OrdinalIgnoreCase) == 0) {
                    return (ErrorTypes)Enum.Parse(typeof(ErrorTypes), f.Name);
                }
            }

            return ErrorTypes.Unknow;
        }


        public static string ClearHtml(this string html) {
            if (string.IsNullOrWhiteSpace(html))
                return html;

            html = HtmlClearReg.Replace(html, Environment.NewLine);
            var parser = new HtmlParser(); ;
            using (var doc = parser.Parse(html)) {
                return doc.DocumentElement.TextContent;
            }
        }
    }
}
