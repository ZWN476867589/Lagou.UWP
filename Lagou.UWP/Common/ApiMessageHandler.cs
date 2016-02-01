using Caliburn.Micro;
using Lagou.API;
using Lagou.UWP.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace Lagou.UWP.Common {
    public class ApiMessageHandler {

        public static void Init() {
            API.ApiClient.OnMessage += ApiClient_OnMessage;
        }

        private static async void ApiClient_OnMessage(object sender, API.MessageArgs e) {
            await DispatcherHelper.Run(() => DealMessage(e));
        }

        private static async void DealMessage(MessageArgs e) {
            switch (e.ErrorType) {
                case ErrorTypes.NeedLogin:
                    var ns = IoC.Get<INavigationService>();
                    //ns.SuspendState();
                    ns.For<LoginViewModel>().Navigate();
                    break;
                case ErrorTypes.DNSError:
                case ErrorTypes.Network:
                    var dialog = new MessageDialog("当前网络不可用,请检查", "网络异常");
                    await dialog.ShowAsync();
                    break;
                default:
                    var dialog2 = new MessageDialog(e.Message, "提示");
                    await dialog2.ShowAsync();
                    break;
            }
        }
    }
}
