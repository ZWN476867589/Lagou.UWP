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

namespace Lagou.UWP.ViewModels {
    [Regist(InstanceMode.Singleton)]
    public class RootFrameViewModel : ViewAware {

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

        public Thickness Margin { get; set; }

        public RootFrameViewModel() {
            DisplayInformation.GetForCurrentView().OrientationChanged += RootFrameViewModel_OrientationChanged;
            this.UpdateStatusBarHeight();
        }

        private void RootFrameViewModel_OrientationChanged(DisplayInformation sender, object args) {
            this.UpdateStatusBarHeight();
        }

        private void UpdateStatusBarHeight() {
            var rect = StatusBar.GetForCurrentView().OccludedRect;
            var org = DisplayInformation.GetForCurrentView().CurrentOrientation;
            switch (org) {
                case DisplayOrientations.Landscape:
                    //横向
                    this.Margin = new Thickness(rect.Width, 0, 0, 0);
                    break;
                case DisplayOrientations.LandscapeFlipped:
                    //横向 180度
                    this.Margin = new Thickness(0, 0, rect.Width, 0);
                    break;
                case DisplayOrientations.PortraitFlipped:
                    //垂直，倒置
                    this.Margin = new Thickness(0, 0, 0, rect.Height);
                    break;
                default:
                    this.Margin = new Thickness(0, rect.Height, 0, 0);
                    break;
            }

            this.NotifyOfPropertyChange(() => this.Margin);
        }
    }
}
