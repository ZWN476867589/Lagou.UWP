using Lagou.API.Attributes;
using Lagou.API.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagou.API.Methods {
    public class Search : MethodBase<IEnumerable<PositionBrief>> {
        public override string Module {
            get {
                return "custom/search.json";
            }
        }

        protected override IEnumerable<PositionBrief> DefaultValue {
            get {
                return Enumerable.Empty<PositionBrief>();
            }
        }

        [Param("pageNo")]
        public int Page {
            get; set;
        } = 1;


        [Param("pageSize")]
        public int PageSize { get; set; } = 20;


        [Param("city")]
        public string City { get; set; }

        [Param("positionName")]
        public string Key { get; set; }

        protected override IEnumerable<PositionBrief> Execute(string result) {
            var o = new {
                content = new {
                    data = new {
                        page = new {
                            result = Enumerable.Empty<PositionBrief>(),
                            pageNo = 1,
                            pageSize = 1,
                            totalCount = 0
                        }
                    }
                }
            };

            o = JsonConvert.DeserializeAnonymousType(result, o);
            return o.content.data.page.result;
        }
    }
}
