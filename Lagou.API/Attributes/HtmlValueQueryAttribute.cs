using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Dom;

namespace Lagou.API.Attributes {

    public enum HtmlQueryValueTargets {
        Text,
        InnerHtml,
        Attribute
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class HtmlValueQueryAttribute : HtmlParserAttribute {


        public HtmlQueryValueTargets ValueTarget { get; private set; }

        public string AttributeName { get; set; }

        public HtmlValueQueryAttribute(string selector, HtmlQueryValueTargets target = HtmlQueryValueTargets.Text, string attrName = null)
            : base(selector) {

            this.ValueTarget = target;
            this.AttributeName = attrName;
        }

        public override object Parse(IParentNode node, Type valueType) {
            var ele = node.QuerySelector(this.Selector);
            if (ele != null) {
                string value = "";
                switch (this.ValueTarget) {
                    case HtmlQueryValueTargets.Attribute:
                        var a = ele.Attributes.GetNamedItem(this.AttributeName);
                        if (a != null)
                            value = a.Value.Trim();
                        break;
                    case HtmlQueryValueTargets.InnerHtml:
                        value = ele.InnerHtml.Trim();
                        break;
                    case HtmlQueryValueTargets.Text:
                        value = ele.TextContent.Trim();
                        break;
                }

                return value;
            }

            return null;
        }
    }
}
