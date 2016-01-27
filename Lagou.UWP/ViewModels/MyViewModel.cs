using Caliburn.Micro;
using Lagou.UWP.Attributes;
using Lagou.UWP.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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

        public ICommand LoginCmd { get; set; }

        public ICommand ViewResumeCmd { get; set; }

        public MyViewModel(INavigationService ns) {
            this.LoginCmd = new Command(() => {
                ns.For<LoginViewModel>()
                .Navigate();
            });

            this.ViewResumeCmd = new Command(() => {
                ns.For<ResumeViewModel>()
                .Navigate();
            });
        }
    }
}
