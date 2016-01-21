using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagou.API.Attributes {

    public enum HtmlQueryValueTargets {
        Text,
        InnerHtml,
        Attribute
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class HtmlQueryAttribute : Attribute {

        public string Selector { get; private set; }
        public HtmlQueryValueTargets ValueTarget { get; private set; }

        public string AttributeName { get; set; }

        public HtmlQueryAttribute(string selector, HtmlQueryValueTargets target = HtmlQueryValueTargets.Text, string attrName = null) {
            this.Selector = selector;
            this.ValueTarget = target;
            this.AttributeName = attrName;
        }

    }
}
