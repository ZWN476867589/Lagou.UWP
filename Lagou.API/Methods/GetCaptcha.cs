using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Lagou.API.Methods {
    public class GetCaptcha : MethodBase<byte[]> {
        public override string Module {
            get {
                return "https://passport.lagou.com/vcode/create?from=register";
            }
        }

        protected override bool WithCookies {
            get {
                return true;
            }
        }

        internal async override Task<byte[]> Execute(ApiClient client) {
            var url = this.BuildUrl(client.GetUrl(this));

            using (var handler = new HttpClientHandler() {
                //CookieContainer = client.Cookies,
                UseCookies = this.WithCookies
            })
            using (HttpClient hc = new HttpClient(handler)) {
                return await hc.GetByteArrayAsync(url);
            }
        }
    }
}
