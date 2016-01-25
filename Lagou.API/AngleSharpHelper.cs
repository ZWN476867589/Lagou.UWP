using AngleSharp.Parser.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Lagou.API.Attributes;
using AngleSharp.Dom;

namespace Lagou.API {
    public static class AngleSharpHelper {

        public static T Parse<T>(this HtmlParser parser, string source) where T : class, new() {

            using (var doc = parser.Parse(source)) {

                var t = new T();
                var ps = typeof(T).GetRuntimeProperties();
                foreach (var p in ps) {
                    var value = p.Parse(doc);
                    try {
                        if (!p.PropertyType.Equals(typeof(string))) {
                            var v = Convert.ChangeType(value, p.PropertyType);
                            p.SetValue(t, v);
                        } else {
                            p.SetValue(t, value);
                        }
                    } catch { }
                }
                return t;
            }
        }

        public static object Parse(this PropertyInfo p, IParentNode node) {
            var attr = p.GetCustomAttribute<HtmlParserAttribute>();
            if (attr != null) {
                var value = attr.Parse(node, p.PropertyType);
                return value;
            }

            return null;
        }
    }
}
