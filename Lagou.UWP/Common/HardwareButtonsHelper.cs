using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Metadata;
using Windows.Phone.UI.Input;

namespace Lagou.UWP.Common {
    public static class HardwareButtonsHelper {

        private static int _blackBtnClickCount = 0;

        public static void Init() {
            if (ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")) {
                HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            }
        }

        private static void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e) {
            var ns = IoC.Get<INavigationService>();
            var c = ns.BackStack.Count;
            if (!ns.CanGoBack) {
                Task.Delay(1000).ContinueWith(t => {
                    _blackBtnClickCount = 0;
                });

                if (_blackBtnClickCount == 2) {
                    App.Current.Exit();
                } else
                    e.Handled = true;
            }
        }
    }
}
