using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Lagou.API.Attributes {

    public enum EnumUseNameOrValue {
        Name,
        Value
    }


    /// <summary>
    /// 枚举参数
    /// </summary>
    public class EnumParamAttribute : ParamAttribute {

        private EnumUseNameOrValue Use;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">参数名</param>
        /// <param name="use">使用枚举的名称还是值</param>
        public EnumParamAttribute(string name, EnumUseNameOrValue use)
            : base(name) {

            this.Use = use;
        }

        public override Dictionary<string, string> GetParams(object obj, System.Reflection.PropertyInfo p) {
            var value = p.GetValue(obj, null);
            if (value != null) {
                var attr = value.GetType()
                        .GetRuntimeField(value.ToString())
                        .GetCustomAttributes(false)
                        .OfType<SpecifyNameValueAttribute>().FirstOrDefault();

                if (attr != null) {
                    value = this.Use == EnumUseNameOrValue.Name ? (object)attr.Name : attr.Value;
                } else {
                    var t = p.PropertyType;
                    var tf = t.GetTypeInfo();
                    if (tf.IsGenericType && tf.GetGenericTypeDefinition().Equals(typeof(Nullable<>))) {
                        t = Nullable.GetUnderlyingType(t);
                    }
                    value = this.Use == EnumUseNameOrValue.Name ? Enum.GetName(t, value) : value;
                }
            }


            if (value == null && this.Required)
                return new Dictionary<string, string>(){
                    {this.Name, ""}
                };
            else if (value == null && !this.Required)
                return null;
            else
                return new Dictionary<string, string>(){
                    {this.Name, value.ToString()}
                };
        }

    }
}
