using AngleSharp.Parser.Html;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lagou.API.Converters {
    public class HtmlCleanConverter : JsonConverter {

        private static readonly Regex RX = new Regex(@"<br[\s]*/?>", RegexOptions.IgnoreCase);

        public override bool CanConvert(Type objectType) {
            return objectType.Equals(typeof(string));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            var str = (string)reader.Value;
            return str.ClearHtml();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            throw new NotImplementedException();
        }
    }
}
