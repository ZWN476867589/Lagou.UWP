using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Lagou.API {
    public class MessageArgs : EventArgs {
        public ErrorTypes ErrorType {
            get;
            set;
        }

        public string Message {
            get;
            set;
        }
    }

    public class ApiClient {

        private static ApiClient Instance = null;
        private static object LockObj = new Object();

        public static event EventHandler<MessageArgs> OnMessage;

        private static ApiClient GetInstance() {
            if (Instance == null) {
                lock (LockObj) {
                    Instance = new ApiClient();
                }
            }
            return Instance;
        }


        //internal CookieContainer Cookies = new CookieContainer();
        //public bool IsLogined {
        //    get {
        //        var cks = this.Cookies.GetCookies(new Uri("https://passport.lagou.com"));
        //        return cks.Cast<Cookie>().Any(c => c.Name.Equals("ticketGrantingTicketId"));
        //    }
        //}


        private ApiClient() {

        }

        public string GetUrl(MethodBase method) {
            //return string.Format("http://www.lagou.com/{0}", method.Module);
            return method.Module.FixUrl("http://www.lagou.com");
        }

        public static async Task<TResult> Execute<TResult>(MethodBase<TResult> method) {
            return await ApiClient.GetInstance()
                .InnerExecute(method)
                .ContinueWith((t, m) => {
                    var mt = (MethodBase)m;
                    if (mt.HasError && OnMessage != null)
                        OnMessage.Invoke(null, new MessageArgs() {
                            ErrorType = mt.ErrorType ?? ErrorTypes.Unknow,
                            Message = mt.Message
                        });
                    return t.Result;
                }, method);
        }

        private async Task<TResult> InnerExecute<TResult>(MethodBase<TResult> method) {
            return await method.Execute(this);
        }
    }
}
