using AngleSharp.Parser.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagou.API.Attributes {
    public class HtmlClearAttribute : ClearAttribute {
        public override string Clear(string str) {
            var parser = new HtmlParser(); ;
            using(var doc = parser.Parse(str)) {
                return doc.DocumentElement.TextContent;
            }
        }
    }
}
