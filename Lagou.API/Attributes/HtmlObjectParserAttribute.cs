using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Dom;

namespace Lagou.API.Attributes {
    public class HtmlObjectParserAttribute : HtmlParserAttribute {

        public HtmlObjectParserAttribute(string selector)
            : base(selector) { }

        public override object Parse(IParentNode node, Type valueType) {
            throw new NotImplementedException();
        }
    }
}
