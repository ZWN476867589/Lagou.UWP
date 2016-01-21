using AngleSharp.Parser.Html;
using Lagou.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lagou.API.Methods {
    public class PositionDetail : MethodBase<Position> {
        public override string Module {
            get {
                return string.Format("center/job_{0}.html?m=1", this.PositionID);
            }
        }

        public int PositionID { get; set; }


        private static readonly Regex AddressRx = new Regex(@"global.companyAddress\s*=\s*(['""])(?<v>[\s\S]*?)\1;", RegexOptions.IgnoreCase);
        private static readonly Regex CompanyIDRx = new Regex(@"global.companyId\s*=\s*(['""])(?<v>[\s\S]*?)\1;", RegexOptions.IgnoreCase);
        private static readonly Regex PositionIDRx = new Regex(@"global.positionId\s*=\s*(['""])(?<v>[\s\S]*?)\1;", RegexOptions.IgnoreCase);


        protected override Position Execute(string result) {
            var parser = new HtmlParser();
            var job = parser.Parse<Position>(result);
            job.CompanyLogo = job.CompanyLogo.FixUrl("http://www.lagou.com");
            job.Temptation = Regex.Replace(job.Temptation, "^职位诱惑：", "");

            if (CompanyIDRx.IsMatch(result))
                job.CompanyID = CompanyIDRx.Match(result).Groups["v"].Value.ToInt(0);

            if (PositionIDRx.IsMatch(result))
                job.PositionID = PositionIDRx.Match(result).Groups["v"].Value.ToInt();

            if (AddressRx.IsMatch(result))
                job.CompanyAddress = AddressRx.Match(result).Groups["v"].Value;

            return job;
        }
    }
}
