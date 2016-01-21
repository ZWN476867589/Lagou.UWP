using Lagou.UWP.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagou.UWP.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class SearchViewModel : BasePageVM {
        public override char Glyph {
            get {
                return (char)0xf002;// "&#xf002;";
            }
        }

        public override string Title {
            get {
                return "职位搜索";
            }
        }
    }
}
