using Lagou.UWP.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagou.UWP.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class MyViewModel : BasePageVM {
        public override char Glyph {
            get {
                return (char)0xf0f0;// "&#xf0f0;";
            }
        }

        public override string Title {
            get {
                return "我的信息";
            }
        }

        protected override void OnActivate() {
            base.OnActivate();
        }
    }
}
