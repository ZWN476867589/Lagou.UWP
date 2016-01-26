using AngleSharp.Parser.Html;
using Lagou.API.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lagou.API.Methods {
    /// <summary>
    /// 面试评价列表
    /// </summary>
    public class EvaluationList : MethodBase<IEnumerable<Evaluation>> {
        public override string Module {
            get {
                return string.Format("/center/expsList_{0}.html", this.PositionID);
            }
        }

        protected override IEnumerable<Evaluation> DefaultValue {
            get {
                return Enumerable.Empty<Evaluation>();
            }
        }

        public int PositionID { get; set; }

        private Regex Rx = new Regex(@"global.page[\s\S]*?(?<json>{[\s\S]*?});[\s\S]*global.");

        protected override IEnumerable<Evaluation> Execute(string result) {
            if (Rx.IsMatch(result)) {
                var json = Rx.Match(result).Groups["json"].Value;
                var o = new {
                    result = Enumerable.Empty<Evaluation>()
                };
                o = JsonConvert.DeserializeAnonymousType(json, o);
                return o.result;
            } else {
                this.ErrorType = ErrorTypes.MatchError;
                return Enumerable.Empty<Evaluation>();
            }
        }
    }
}
