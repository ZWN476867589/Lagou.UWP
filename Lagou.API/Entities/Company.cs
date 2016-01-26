using Lagou.API.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagou.API.Entities {
    public class Company {

        [HtmlValueParser(".company_info .company_main h1 a")]
        public string CompanyName { get; set; }

        [HtmlValueParser(".company_info .company_main h1 a", HtmlQueryValueTargets.Attribute, AttributeName = "title")]
        public string FullName { get; set; }

        [HtmlValueParser(".company_info .company_main h1 a", HtmlQueryValueTargets.Attribute, AttributeName = "href")]
        public string Website { get; set; }

        [HtmlValueParser(".company_info .company_main .company_word")]
        public string ShortDesc { get; set; }


        [HtmlClear]
        [HtmlValueParser("#company_intro .company_content", HtmlQueryValueTargets.InnerHtml)]
        public string Desc { get; set; }

        [HtmlValueParser("#basic_container .type + span")]
        public string CompanyType { get; set; }

        [HtmlValueParser("#basic_container .process + span")]
        public string RongZiJinDu { get; set; }

        /// <summary>
        /// 企业人员规模（人数）
        /// </summary>
        [HtmlValueParser("#basic_container .number + span")]
        public string Scale { get; set; }

        [HtmlValueParser("#basic_container .address + span")]
        public string Address { get; set; }


        [HtmlObjectCollecionParser(".company_managers .manager_list .rotate_item")]
        public IEnumerable<Manager> Managers { get; set; }

        [HtmlObjectCollecionParser("#company_products .product_item")]
        public IEnumerable<Product> Products { get; set; }
    }



    public class Manager {

        [HtmlValueParser(".item_manger_photo_show", HtmlQueryValueTargets.Attribute, "src")]
        public string ImgSrc { get; set; }

        [HtmlValueParser(".item_manager_name span")]
        public string Name { get; set; }

        [HtmlValueParser(".item_manager_title")]
        public string Title { get; set; }


        [HtmlClear]
        [HtmlValueParser(".item_manager_content", HtmlQueryValueTargets.InnerHtml)]
        public string Desc { get; set; }
    }

    public class Product {
        [HtmlValueParser("img", HtmlQueryValueTargets.Attribute, "src")]
        public string Img { get; set; }

        [HtmlValueParser(".product_url a", HtmlQueryValueTargets.Attribute, "href")]
        public string Url { get; set; }

        [HtmlValueParser(".product_url a", HtmlQueryValueTargets.Text)]
        public string Name { get; set; }

        [HtmlValueCollectionParserAttribute("ul.clearfix li", HtmlQueryValueTargets.Text)]
        public List<string> Types { get; set; }

        [HtmlClear]
        [HtmlValueParser(".product_profile")]
        public string Desc { get; set; }
    }
}
