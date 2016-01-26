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


    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class HtmlObjectCollecionParserAttribute : HtmlParserAttribute {

        public HtmlObjectCollecionParserAttribute(string selector)
            : base(selector) {

        }

        public override object Parse(IParentNode node, Type valueType) {
            var ti = valueType.GetTypeInfo();
            if (!typeof(IEnumerable).GetTypeInfo().IsAssignableFrom(ti)) {
                throw new NotSupportedException("HtmlCollectionQueryAttribute 只支持非基元/非字符串的集合类型");
            }
            var type = ti.GenericTypeArguments[0];

            if (type.GetTypeInfo().IsPrimitive || type.Equals(typeof(string))) {
                throw new NotSupportedException("HtmlCollectionQueryAttribute 只支持非基元/非字符串的集合类型");
            }
            
            //object parser
            var value = this.ParseSub(type, node);
            return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(value), valueType);
        }

        private IEnumerable<object> ParseSub(Type t, IParentNode node) {
            var values = new List<object>();
            //var ttt = typeof(IList<>).MakeGenericType(t);

            var eles = node.QuerySelectorAll(this.Selector);
            var ps = t.GetRuntimeProperties();
            foreach (var ele in eles) {
                var o = Activator.CreateInstance(t);

                var obj = Convert.ChangeType(o, t);

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
