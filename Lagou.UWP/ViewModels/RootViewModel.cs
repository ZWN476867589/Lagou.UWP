using Caliburn.Micro;
using Lagou.UWP.Attributes;
using Lagou.UWP.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Lagou.UWP.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class RootViewModel : Screen {

        public SearchBarViewModel SearchBarVM { get; set; } = new SearchBarViewModel();

        private WinRTContainer Container = null;

        public RootViewModel(WinRTContainer container) {
            this.Container = container;
        }

        public void FrameLoaded(Frame frm) {
            var ns = this.Container.RegisterNavigationService(frm);

            //ns.For<ShellViewModel>()
            //    .Navigate();

            var m = ViewLocator.LocateTypeForModelType(typeof(ShellViewModel), null, null);
            ns.For<ShellViewModel>()
                .Navigate();
        }
    }
}
