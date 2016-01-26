using Lagou.API.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagou.API.Entities {
    /// <summary>
    /// 
    /// </summary>
    public class Position {
        
        public int CompanyID { get; set; }

        public int PositionID { get; set; }

        public string CompanyAddress { get; set; }

        /// <summary>
        /// 职位名称
        /// </summary>
        [HtmlValueParser(".postitle .title")]
        public string JobTitle { get; set; }

        /// <summary>
        /// 薪水
        /// </summary>
        [HtmlValueParser(".detail .items .salary")]
        public string Salary { get; set; }

        /// <summary>
        /// 工作地点
        /// </summary>
        [HtmlValueParser(".detail .items .workaddress")]
        public string WorkAddress { get; set; }

        /// <summary>
        /// 全职/兼职
        /// </summary>
        [HtmlValueParser(".detail .items .jobnature")]
        public string JobNature { get; set; }

        /// <summary>
        /// 工作年限
        /// </summary>
        [HtmlValueParser(".detail .items .workyear")]
        public string WorkYear { get; set; }

        /// <summary>
        /// 学历
        /// </summary>
        [HtmlValueParser(".detail .items .education")]
        public string Education { get; set; }

        /// <summary>
        /// 职位诱惑
        /// </summary>
        [HtmlValueParser(".detail .temptation")]
        public string Temptation { get; set; }

        [HtmlValueParser(".company .logo", HtmlQueryValueTargets.Attribute, "src")]
        public string CompanyLogo { get; set; }

        [HtmlValueParser(".company .desc .title")]
        public string CompanyName { get; set; }

        [HtmlValueParser(".company .desc .info")]
        public string CompanyDesc { get; set; }


        /// <summary>
        /// 职位描述
        /// </summary>
        [HtmlValueParser(".positiondesc .content")]
        public string JobDesc { get; set; }
    }
}
