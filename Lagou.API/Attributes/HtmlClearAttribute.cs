using AngleSharp.Parser.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lagou.API.Attributes {
    public class HtmlClearAttribute : ClearAttribute {

        private static readonly Regex RX = new Regex(@"<br[\s]*/?>", RegexOptions.IgnoreCase);

        public override string Clear(string str) {
            return str.ClearHtml();
        }
    }
}
