using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;

namespace Lagou.UWP.Common {
    public static class DispatcherHelper {

        private static Lazy<CoreDispatcher> Dispatcher = new Lazy<CoreDispatcher>(() => {
            return CoreApplication.MainView.CoreWindow.Dispatcher;
            //return CoreApplication.GetCurrentView().Dispatcher;
        });

        public static async Task Run(Action act) {
            await Dispatcher.Value.RunAsync(CoreDispatcherPriority.High, () => {
                act.Invoke();
            });
        }

    }
}
