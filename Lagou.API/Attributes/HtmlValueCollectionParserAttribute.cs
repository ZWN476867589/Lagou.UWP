using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Dom;
using System.Reflection;
using System.Collections;
using Newtonsoft.Json;

namespace Lagou.API.Attributes {
    public class HtmlValueCollectionParserAttribute : HtmlValueParserAttribute {
        public HtmlValueCollectionParserAttribute(string selector, HtmlQueryValueTargets target = HtmlQueryValueTargets.Text, string attrName = null)
            : base(selector, target, attrName) {
        }

        public override object Parse(IParentNode node, Type valueType) {
            var ti = valueType.GetTypeInfo();
            if (!typeof(IEnumerable).GetTypeInfo().IsAssignableFrom(ti)) {
                throw new NotSupportedException("HtmlValueCollectionParserAttribute 只支持基元类型与string的集合类型");
            }

            var type = ti.GenericTypeArguments[0];
            if (!type.GetTypeInfo().IsPrimitive && !type.Equals(typeof(string))) {
                throw new NotSupportedException("HtmlValueCollectionParserAttribute 只支持基元类型与string的集合类型");
            }


            var values = new List<object>();

            var eles = node.QuerySelectorAll(this.Selector);
            foreach (var ele in eles) {
                var value = base.Parse(ele, type);
                values.Add(value);
            }

            return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(values), valueType);
        }
    }
}
