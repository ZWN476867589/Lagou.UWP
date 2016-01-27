using Lagou.API;
using Lagou.API.Methods;
using Lagou.UWP.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagou.UWP.ViewModels {
    [Regist(InstanceMode.Singleton)]
    public class ResumeViewModel : BasePageVM {
        public override string Title {
            get {
                return "我的简历";
            }
        }

        public override char Glyph {
            get {
                return char.MinValue;
            }
        }

        public string Data { get; set; }



        protected override void OnActivate() {
            base.OnActivate();
            this.LoadResume();
        }

        private async void LoadResume() {
            this.IsBusy = true;

            var mth = new ResumePreview();
            var html = await ApiClient.Execute(mth);

            //this.Data = new HtmlWebViewSource() {
            //    Html = html
            //};
            this.Data = html;

            this.NotifyOfPropertyChange(() => this.Data);

            this.IsBusy = false;
        }
    }
}
