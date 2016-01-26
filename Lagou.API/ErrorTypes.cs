using Lagou.API.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagou.API {
    public enum ErrorTypes {
        Unknow,

        NeedLogin,

        /// <summary>
        /// 404
        /// </summary>
        [ErrorTag("404")]
        HttpNotFound,

        /// <summary>
        /// 500
        /// </summary>
        [ErrorTag("500")]
        ServerException,

        [ErrorTag("-2146233079")]
        DNSError,

        [ErrorTag("-2147012889")]
        Network,

        LoginFailed,

        /// <summary>
        /// 内容匹配错误
        /// </summary>
        MatchError
    }
}
