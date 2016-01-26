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
    public class CompanyDetail : MethodBase<Company2> {
        public override string Module {
            get {
                return $"http://www.lagou.com/gongsi/{this.CompanyID}.html";
            }
        }

        public int CompanyID { get; set; }

        private static readonly Regex Reg = new Regex(@"<script id=""companyInfoData"" type=""text/html"">(?<json>[\s\S]*?)</script>");


        protected override Company2 Execute(string result) {
            //var parser = new HtmlParser();
            //var company = parser.Parse<Company>(result);

            //return company;

            if (Reg.IsMatch(result)) {
                var json = Reg.Match(result).Groups["json"].Value;
                return JsonConvert.DeserializeObject<Company2>(json);
            } else {
                this.ErrorType = ErrorTypes.MatchError;
                return null;
            }
        }
    }
}
