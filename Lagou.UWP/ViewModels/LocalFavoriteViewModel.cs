using Lagou.UWP.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagou.UWP.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class LocalFavoriteViewModel : BasePageVM {
        public override char Glyph {
            get {
                return (char)0xf005;// "&#xf005;";
            }
        }

        public override string Title {
            get {
                return "本地收藏";
            }
        }
    }
}
