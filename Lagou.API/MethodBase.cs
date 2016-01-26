using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Lagou.API {
    public abstract class MethodBase {

        /// <summary>
        /// 模块
        /// </summary>
        public abstract string Module {
            get;
        }

        public bool HasError {
            get {
                return this.ErrorType != null;
            }
        }

        public ErrorTypes? ErrorType {
            get;
            set;
        }

        /// <summary>
        /// 执行消息
        /// </summary>
        public string Message {
            get;
            set;
        }

        protected virtual bool WithCookies {
            get {
                return false;
            }
        }

        public async virtual Task<string> GetResult(ApiClient client) {
            try {
                var url = this.BuildUrl(client.GetUrl(this));
                using (var handler = new HttpClientHandler() {
                    //CookieContainer = client.Cookies,
                    UseCookies = this.WithCookies
                })
                using (HttpClient hc = new HttpClient(handler)) {
                    return await hc.GetStringAsync(url);
                }
            } catch (Exception ex) {
                var bex = ex.GetBaseException();

                this.ErrorType = bex.HResult.ToString().ParseErrorType();
                this.Message = bex.Message;
                return "";
            }
        }
    }

    public abstract class MethodBase<TResult> : MethodBase {

        protected virtual TResult DefaultValue { get; } = default(TResult);

        internal virtual async Task<TResult> Execute(ApiClient client) {
            var result = await this.GetResult(client);
            if (!this.HasError)
                return this.Execute(result);
            else
                return this.DefaultValue;
        }

        protected virtual TResult Execute(string result) {
            return JsonConvert.DeserializeObject<TResult>(result);
        }
    }
}
