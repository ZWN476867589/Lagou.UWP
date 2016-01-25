using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Dom;
using System.Reflection;
using System.Collections;

namespace Lagou.API.Attributes {


    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class HtmlCollecionQueryAttribute : HtmlParserAttribute {

        public HtmlCollecionQueryAttribute(string selector)
            : base(selector) {

        }

        public override object Parse(IParentNode node, Type valueType) {
            var ti = valueType.GetTypeInfo();
            if (!typeof(IEnumerable).GetTypeInfo().IsAssignableFrom(ti)) {
                throw new NotSupportedException("HtmlCollectionQueryAttribute 只支持集合类型");
            }
            var type = ti.GenericTypeArguments[0];

            if (type.IsByRef && !type.Equals(typeof(string))) {
                // value parser
            } else {
                //object parser
                return this.ParseSub(type, node);
            }
            return null;
        }

        private IEnumerable<object> ParseSub(Type t, IParentNode node) {
            var values = new List<object>();

            var eles = node.QuerySelectorAll(this.Selector);
            var ps = t.GetRuntimeProperties();
            foreach (var ele in eles) {
                var obj = Activator.CreateInstance(t);

                foreach (var p in ps) {
                    var value = p.Parse(ele);
                    p.SetValue(obj, value);
                }

                values.Add(obj);
            }

            return values;
        }
    }
}
