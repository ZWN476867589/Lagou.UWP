using Lagou.API.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagou.API.Entities {


    public class Company2 {
        public _History[] History { get; set; }
        public _CoreInfo CoreInfo { get; set; }
        public _Location[] Location { get; set; }
        public string[] Labels { get; set; }
        public _DataInfo DataInfo { get; set; }
        public int CompanyId { get; set; }
        public _BaseInfo BaseInfo { get; set; }
        public _Leader[] Leaders { get; set; }
        public _Product[] Products { get; set; }
        public _Introduction Introduction { get; set; }


        public class _CoreInfo {
            public string Logo { get; set; }

            public string FixedLogo {
                get {
                    return this.Logo.FixUrl("http://www.lagou.com");
                }
            }

            public string CompanyName { get; set; }
            public string CompanyShortName { get; set; }
            public string CompanyUrl { get; set; }

            [JsonProperty("companyIntroduce")]
            public string ShortDesc { get; set; }
        }

        public class _DataInfo {
            /// <summary>
            /// 职位数
            /// </summary>
            public int PositionCount { get; set; }
            /// <summary>
            /// 简历处理率
            /// </summary>
            public int ResumeProcessRate { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public int ResumeProcessTime { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public int ExperienceCount { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string LastLoginTime { get; set; }
        }

        public class _BaseInfo {

            [JsonProperty("industryField")]
            public string BusinessType { get; set; }

            [JsonProperty("companySize")]
            public string EmployeeCount { get; set; }

            public string City { get; set; }

            /// <summary>
            /// 融资进度
            /// </summary>
            public string FinanceStage { get; set; }
        }

        public class _Introduction {
            [JsonProperty("companyProfile"), JsonConverter(typeof(HtmlCleanConverter))]
            public string Desc { get; set; }
            public _Picture[] Pictures { get; set; }
        }

        public class _Picture {

            [JsonProperty("companyPicUrl")]
            public string Src { get; set; }

            public string FixedSrc {
                get {
                    return this.Src.FixUrl("http://www.lagou.com");
                }
            }
        }

        public class _History {
            //部份数据中,这个属性无值
            //[JsonProperty("eventdate")]
            //public string Date { get; set; }

            public string Year { get; set; }
            public string Month { get; set; }
            public string Day { get; set; }

            [JsonProperty("eventname")]
            public string Event { get; set; }

            [JsonProperty("type")]
            public _EventTypes EventType { get; set; }

            [JsonProperty("eventurl")]
            public string Url { get; set; }

            [JsonConverter(typeof(HtmlCleanConverter))]
            public string Eventprofile { get; set; }
            public string FinanceStage { get; set; }
            public string InvestMoney { get; set; }

            public string Summary {
                get {
                    return $"{this.Event} {this.Eventprofile} {this.FinanceStage} {this.InvestMoney}";
                }
            }

        }

        public enum _EventTypes {
            /// <summary>
            /// 数据
            /// </summary>
            Data = 1,
            /// <summary>
            /// 资本
            /// </summary>
            Capital = 2,
            /// <summary>
            /// 人员
            /// </summary>
            Member = 3,

            /// <summary>
            /// 产品
            /// </summary>
            Product = 4,
            Other = 5
        }

        public class _Location {

            [JsonProperty("detailPosition")]
            public string Address { get; set; }
            public bool isdel { get; set; }
            public string Longitude { get; set; }
            public string Latitude { get; set; }
        }

        public class _Leader {
            [JsonProperty("photo")]
            public string Img { get; set; }

            public string FixedImg {
                get {
                    return this.Img.FixUrl("http://www.lagou.com");
                }
            }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("position")]
            public string Title { get; set; }

            public string Weibo { get; set; }

            [JsonConverter(typeof(HtmlCleanConverter))]
            public string Remark { get; set; }
        }


        public class _Product {

            [JsonProperty("producturl")]
            public string Url { get; set; }

            [JsonProperty("productprofile"), JsonConverter(typeof(HtmlCleanConverter))]
            public string Desc { get; set; }

            [JsonProperty("product")]
            public string Name { get; set; }

            [JsonProperty("productpicurl")]
            public string Img { get; set; }

            public string FixedImg {
                get {
                    return this.Img.FixUrl("http://www.lagou.com");
                }
            }

            [JsonProperty("producttype")]
            public string[] Type { get; set; }
        }
    }

}