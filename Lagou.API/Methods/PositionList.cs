using Lagou.API.Attributes;
using Lagou.API.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lagou.API.Methods {
    public class PositionList : MethodBase<IEnumerable<PositionBrief>> {

        private static readonly Regex rx = new Regex(@"(?<ctx>{""pageSize""[\w\W]*?""pageNo"":\d+})}}", RegexOptions.IgnoreCase);

        public override string Module {
            get {
                return "center/companyjobs.json";
            }
        }

        protected override IEnumerable<PositionBrief> DefaultValue {
            get {
                return Enumerable.Empty<PositionBrief>();
            }
        }

        [Param("pageNo")]
        public int Page { get; set; } = 1;

        [Param("pageSize")]
        public int PageSize { get; set; } = 20;

        [Param("companyId", Required = true)]
        public int CompanyID { get; set; }

        [EnumParam("positionFirstType", EnumUseNameOrValue.Name)]
        public PositionTypes? PositionType { get; set; }

        /// <summary>
        /// 总条数
        /// </summary>
        public int Total { get; private set; }

        protected override IEnumerable<PositionBrief> Execute(string result) {
            if (rx.IsMatch(result)) {
                var ctx = rx.Match(result).Groups["ctx"].Value;
                var o = new {
                    result = Enumerable.Empty<PositionBrief>(),
                    totalCount = 0
                };

                o = JsonConvert.DeserializeAnonymousType(ctx, o);
                this.Total = o.totalCount;
                return o.result;
            } else {
                this.ErrorType = ErrorTypes.MatchError;
                return Enumerable.Empty<PositionBrief>();
            }
        }
    }
}
