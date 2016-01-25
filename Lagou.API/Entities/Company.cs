using Lagou.API.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagou.API.Entities {
    public class Company {

        [HtmlValueQuery(".company_info .company_main h1 a")]
        public string CompanyName { get; set; }

        [HtmlValueQuery(".company_info .company_main h1 a", HtmlQueryValueTargets.Attribute, AttributeName = "title")]
        public string FullName { get; set; }

        [HtmlValueQuery(".company_info .company_main h1 a", HtmlQueryValueTargets.Attribute, AttributeName = "href")]
        public string Website { get; set; }

        [HtmlValueQuery(".company_info .company_main .company_word")]
        public string ShortDesc { get; set; }

        [HtmlValueQuery("#company_intro .company_content", HtmlQueryValueTargets.InnerHtml)]
        public string Desc { get; set; }

        [HtmlValueQuery("#basic_container .type + span")]
        public string CompanyType { get; set; }

        [HtmlValueQuery("#basic_container .process + span")]
        public string RongZiJinDu { get; set; }

        /// <summary>
        /// 企业人员规模（人数）
        /// </summary>
        [HtmlValueQuery("#basic_container .number + span")]
        public string Scale { get; set; }

        [HtmlValueQuery("#basic_container .address + span")]
        public string Address { get; set; }


        [HtmlCollecionQuery(".company_managers .manager_list .rotate_item")]
        public IEnumerable<Manager> Managers { get; set; }
    }



    public class Manager {

        [HtmlValueQuery(".item_manger_photo_show", HtmlQueryValueTargets.Attribute, "src")]
        public string ImgSrc { get; set; }

        [HtmlValueQuery(".item_manager_name span")]
        public string Name { get; set; }

        [HtmlValueQuery(".item_manager_title")]
        public string Title { get; set; }

        [HtmlValueQuery(".item_manager_content", HtmlQueryValueTargets.InnerHtml)]
        public string Desc { get; set; }
    }
}
