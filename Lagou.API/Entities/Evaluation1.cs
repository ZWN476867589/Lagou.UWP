using Lagou.API.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagou.API.Entities {
    public class Evaluation {
        public int ID { get; set; }

        public string UserName { get; set; }

        public int PositionId { get; set; }
        public string PositionName { get; set; }
        public string PositionType { get; set; }

        public int CompanyId { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 公司环境分
        /// </summary>
        public int CompanyScore { get; set; }

        /// <summary>
        /// 综合评分
        /// </summary>
        public float ComprehensiveScore { get; set; }

        /// <summary>
        /// 评价内容
        /// </summary>
        public string Content { get; set; }

        [JsonProperty("createTime"), JsonConverter(typeof(UnixTimestampDateTimeConverter))]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 描述符合分
        /// </summary>
        public int DescribeScore { get; set; }

        /// <summary>
        /// 附加评价
        /// </summary>
        [JsonProperty("evaluation")]
        public string OtherEvaluation { get; set; }


        /// <summary>
        /// 面试官分数
        /// </summary>
        public int InterviewerScore { get; set; }

        public List<string> TagArray { get; set; }

        //public int hrId { get; set; }

        //public bool IsAllowReply { get; set; }
        /// <summary>
        /// 是否匿名
        /// </summary>
        //public bool IsAnonymous { get; set; }
        //public bool isInterview { get; set; }
        //public int myScore { get; set; }
        //public string noInterviewReason { get; set; }
        //public int noInterviewType { get; set; }
        //public int orderId { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        //public string Portrait { get; set; }

        //public int replyCount { get; set; }
        //public int source { get; set; }
        //public int status { get; set; }
        //public int type { get; set; }
        //public int usefulCount { get; set; }
        //public int userId { get; set; }
    }
}
