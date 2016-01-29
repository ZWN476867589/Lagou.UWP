using Caliburn.Micro;
using Lagou.API;
using Lagou.API.Methods;
using Lagou.UWP.Attributes;
using Lagou.UWP.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Foundation.Metadata;
using Windows.Phone.UI.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Lagou.UWP.ViewModels {
    [Regist(InstanceMode.Singleton)]
    public class LoginViewModel : BasePageVM, IDisposable {

        public override string Title {
            get {
                return "登陆";
            }
        }

        public override char Glyph {
            get {
                return char.MinValue;
            }
        }

        public string UserName { get; set; }

        public string Pwd { get; set; }

        public bool RememberPwd { get; set; }

        public string Captcha { get; set; }

        public ImageSource CaptchaSource { get; set; }

        public ICommand ReloadCaptcha { get; set; }

        public ICommand LoginCmd { get; set; }

        public ICommand CancelCmd { get; set; }

        private INavigationService NS = null;

        public LoginViewModel(INavigationService ns) {
            this.NS = ns;

            this.ReloadCaptcha = new Command(async () => await this.LoadCaptcha());
            this.LoginCmd = new Command(async () => await this.Login());
            this.CancelCmd = new Command(() => { this.Cancel(); });
        }


        private Stream Stm = null;

        protected async override void OnActivate() {
            this.RememberPwd = Properties.Get<bool>(PropertiesKeys.RememberPwd.ToString());
            if (this.RememberPwd) {
                this.Pwd = Properties.Get<string>(PropertiesKeys.Pwd.ToString());
                this.UserName = Properties.Get<string>(PropertiesKeys.UserName.ToString());
            }
            await this.LoadCaptcha();
        }

        protected override void OnDeactivate(bool close) {
            base.OnDeactivate(close);
            if (this.Stm != null) {
                this.Stm.Dispose();
                this.CaptchaSource = null;
            }
        }

        private async Task LoadCaptcha() {
            var mth = new GetCaptcha();
            var bytes = await ApiClient.Execute(mth);
            if (this.Stm != null)
                this.Stm.Dispose();
            this.Stm = new MemoryStream(bytes);
            //TODO
            var img = new BitmapImage();
            await img.SetSourceAsync(this.Stm.AsRandomAccessStream());
            this.CaptchaSource = img;
            this.NotifyOfPropertyChange(() => this.CaptchaSource);
        }

        private async Task Login() {
            this.IsBusy = true;

            var mth = new Login() {
                UserName = this.UserName ?? "",
                Password = this.Pwd ?? "",
                VerifyCode = this.Captcha ?? ""
            };
            var flag = await ApiClient.Execute(mth);

            this.IsBusy = false;

            if (flag) {
                var dialog = new MessageDialog("登陆成功", "提示");
                await dialog.ShowAsync();
                this.NS.GoBack(2);

                Properties.Set(PropertiesKeys.RememberPwd.ToString(), this.RememberPwd);
                Properties.Set(PropertiesKeys.UserName.ToString(), this.UserName);
                Properties.Set(PropertiesKeys.Pwd.ToString(), this.Pwd);
            } else {
                var dialog = new MessageDialog(mth.Message, "提示");
                await dialog.ShowAsync();
            }
        }


        private void Cancel() {
            //this.NS.ResumeState();
            this.NS.GoBack(2);
        }

        ~LoginViewModel() {
            Dispose(false);
        }

        public void Dispose() {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {
            if (disposing) {
                if (this.Stm != null)
                    this.Stm.Dispose();
            }
        }
    }
}
