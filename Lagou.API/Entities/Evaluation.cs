using Lagou.API.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagou.API.Entities {

    [HtmlQuery("li.list-item")]
    public class Evaluation {
        /// <summary>
        /// 评价人
        /// </summary>
        [HtmlQuery(".info-wrap .name")]
        public string Name { get; set; }

        /// <summary>
        /// 评论时间
        /// </summary>
        [HtmlQuery(".info-wrap .time")]
        public string CreateOn { get; set; }


        [HtmlQuery(".score-wrap li")]
        public IEnumerable<Score> Scores { get; set; }

        [HtmlQuery(".tags-wrap li")]
        public IEnumerable<string> Tags { get; set; }

        [HtmlQuery(".review-content")]
        public string Ctx { get; set; }
    }

    public class Score {
        [HtmlQuery(".type")]
        public string Name { get; set; }

        [HtmlQuery(".score")]
        public double Value { get; set; }
    }
}
