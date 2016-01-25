using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagou.API.Attributes {

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public abstract class HtmlParserAttribute : Attribute {

        public string Selector { get; private set; }

        public HtmlParserAttribute(string selector) {
            this.Selector = selector;
        }

        public abstract object Parse(IParentNode node, Type valueType);
    }
}
