using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Dom;

namespace Lagou.API.Attributes {

    public enum HtmlQueryValueTargets {
        /// <summary>
        /// 标签文本，默认值
        /// </summary>
        Text,
        /// <summary>
        /// 标签内容 HTML
        /// </summary>
        InnerHtml,

        /// <summary>
        /// 标签上的属性
        /// </summary>
        Attribute
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class HtmlValueParserAttribute : HtmlParserAttribute {


        public HtmlQueryValueTargets ValueTarget { get; private set; }

        public string AttributeName { get; set; }

        public HtmlValueParserAttribute(string selector, HtmlQueryValueTargets target = HtmlQueryValueTargets.Text, string attrName = null)
            : base(selector) {

            this.ValueTarget = target;
            this.AttributeName = attrName;
        }

        protected object Parse(IElement ele, Type valueType) {
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

                if (valueType.Equals(typeof(string)))
                    return value;
                else
                    return Convert.ChangeType(value, valueType);
            }
            return null;
        }

        public override object Parse(IParentNode node, Type valueType) {
            var ti = valueType.GetTypeInfo();
            if (!ti.IsPrimitive && !valueType.Equals(typeof(string))) {
                throw new NotSupportedException("HtmlValueQueryAttribute 只支持基元类型");
            }

            var ele = node.QuerySelector(this.Selector);
            return this.Parse(ele, valueType);
        }
    }
}
