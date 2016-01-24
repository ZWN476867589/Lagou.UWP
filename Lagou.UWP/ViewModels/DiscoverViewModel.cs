using Lagou.UWP.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagou.UWP.ViewModels {
    [Regist(InstanceMode.Singleton)]
    public class DiscoverViewModel : BasePageVM {
        public override char Glyph {
            get {
                return (char)0xf1d9;
            }
        }

        public override string Title {
            get {
                return "发现";
            }
        }
    }
}
