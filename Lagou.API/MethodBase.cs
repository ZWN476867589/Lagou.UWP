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

        protected virtual bool NeedLoginFirst {
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

        private async Task PrepareLoginCookie(string url) {
            using (var handler2 = new HttpClientHandler() {
                UseCookies = true,
                AllowAutoRedirect = true
            })
            using (var hc2 = new HttpClient(handler2)) {
                await hc2.GetStringAsync(url);
            }
        }

        public async virtual Task<string> GetResult(ApiClient client) {

            var url = this.BuildUrl(client.GetUrl(this));
            using (var handler = new HttpClientHandler() {
                UseCookies = this.WithCookies,
                AllowAutoRedirect = !this.NeedLoginFirst
            })
            using (HttpClient hc = new HttpClient(handler)) {
                try {
                    var msg = await hc.GetAsync(url);
                    //只有 AllowAutoRedirect 为 false 时,才会捕捉到 Headers.Location
                    if (this.NeedLoginFirst && msg.Headers.Location != null && msg.Headers.Location.Host.Equals("passport.lagou.com")) {
                        this.ErrorType = ErrorTypes.NeedLogin;
                        await this.PrepareLoginCookie(url);
                        return "";
                    } else
                        return await msg.Content.ReadAsStringAsync();

                    //GetStringAsync 会把 302 当作异常抛出
                    //return await hc.GetStringAsync(url);
                } catch (Exception ex) {
                    var bex = ex.GetBaseException();

                    this.ErrorType = bex.HResult.ToString().ParseErrorType();
                    this.Message = bex.Message;
                    return "";
                }
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
