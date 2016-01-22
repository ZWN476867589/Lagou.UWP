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

        /// <summary>
        /// 1
        /// </summary>
        /// <param name="view"></param>
        /// <param name="context"></param>
        protected override void OnViewAttached(object view, object context) {
            base.OnViewAttached(view, context);
        }

        /// <summary>
        /// 2
        /// </summary>
        /// <param name="view"></param>
        protected override void OnViewReady(object view) {
            base.OnViewReady(view);
        }


        /// <summary>
        /// 3
        /// </summary>
        /// <param name="view"></param>
        protected override void OnViewLoaded(object view) {
            base.OnViewLoaded(view);
        }
    }
}
