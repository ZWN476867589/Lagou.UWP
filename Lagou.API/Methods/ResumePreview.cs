using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagou.API.Methods {
    public class ResumePreview : MethodBase<string> {
        public override string Module {
            get {
                return "center/preview.html";
            }
        }

        protected override bool WithCookies {
            get {
                return true;
            }
        }

        protected override string Execute(string result) {
            return result;
        }
    }
}
