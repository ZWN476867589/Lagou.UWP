using Lagou.API.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagou.API.Methods {
    public class Login : MethodBase<bool> {
        public override string Module {
            get {
                return "https://passport.lagou.com/login/login.json";
            }
        }

        protected override bool WithCookies {
            get {
                return true;
            }
        }

        [Param("username", Required = true)]
        public string UserName { get; set; }


        private string pwd = "";
        [Param("password", Required = true)]
        public string Password {
            private get {
                return this.pwd;
            }
            set {
                this.pwd = MD5.GetHashString(string.Format("veenike{0}veenike", MD5.GetHashString(value).ToLower()))
                    .ToLower();
            }
        }

        [Param("request_form_verifyCode")]
        public string VerifyCode { get; set; }


        protected override bool Execute(string result) {
            var o = new { message = "", state = 0 };
            o = JsonConvert.DeserializeAnonymousType(result, o);
            if (o.state != 1) {
                this.Message = o.message;
                this.ErrorType = ErrorTypes.LoginFailed;
                return false;
            } else
                return true;
        }
    }
}
