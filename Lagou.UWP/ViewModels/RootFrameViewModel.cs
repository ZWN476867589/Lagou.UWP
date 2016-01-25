using Caliburn.Micro;
using Lagou.UWP.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.ComponentModel;

namespace Lagou.UWP.ViewModels {
    [Regist(InstanceMode.None)]
    public class RootFrameViewModel : PropertyChangedBase {
        private object _header = null;
        public object Header {
            get {
                return this._header;
            }
            set {
                this._header = value;
                this.NotifyOfPropertyChange(() => this.Header);
            }
        }

        private RootFrameViewModel() { }

        public static Lazy<RootFrameViewModel> Instance =
            new Lazy<RootFrameViewModel>(() => new RootFrameViewModel());
    }
}
