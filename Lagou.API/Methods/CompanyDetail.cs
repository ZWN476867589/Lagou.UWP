using AngleSharp.Parser.Html;
using Lagou.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagou.API.Methods {
    public class CompanyDetail : MethodBase<Company> {
        public override string Module {
            get {
                return $"http://www.lagou.com/gongsi/{this.CompanyID}.html";
            }
        }

        public int CompanyID { get; set; }

        protected override Company Execute(string result) {
            var parser = new HtmlParser();
            var company = parser.Parse<Company>(result);

            return company;
        }
    }
}
