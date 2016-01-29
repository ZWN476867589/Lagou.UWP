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

        public List<Tmp> Datas { get; set; }

        public MyViewModel(INavigationService ns) {
            this.LoginCmd = new Command(() => {
                ns.For<LoginViewModel>()
                .Navigate();
            });

            this.ViewResumeCmd = new Command(() => {
                ns.For<ResumeViewModel>()
                .Navigate();
            });

            this.Datas = new List<Tmp>() {
                new Tmp() {Text = "我的投递", Glyph = (char)0xf21c },
                new Tmp() {Text = "我的面试", Glyph = (char)0xf0c0 },
                new Tmp() {Text = "我的邀约", Glyph = (char)0xf095 },
                new Tmp() {Text = "我的收藏", Glyph = (char)0xf004 },
                new Tmp() {Text = "我的简历", Glyph = (char)0xf1c2, Cmd= this.ViewResumeCmd }
            };
        }

        public class Tmp {
            public char Glyph { get; set; }
            public string Text { get; set; }
            public ICommand Cmd { get; set; }
        }
    }
}
