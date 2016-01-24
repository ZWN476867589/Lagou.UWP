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
    [Regist(InstanceMode.Singleton)]
    public class RootFrameViewModel : DependencyObject, INotifyPropertyChangedEx {

        public static readonly DependencyProperty HeaderTemplateProperty =
            DependencyProperty.Register(
                "HeaderTemplate",
                typeof(DataTemplate),
                typeof(RootFrameViewModel),
                new PropertyMetadata(null));

        public event PropertyChangedEventHandler PropertyChanged;

        public DataTemplate HeaderTemplate {
            get {
                return this.GetValue(HeaderTemplateProperty) as DataTemplate;
            }
            set {
                this.SetValue(HeaderTemplateProperty, value);
                this.NotifyOfPropertyChange("HeaderTemplate");
            }
        }

        //private DataTemplate _headerTemplate = null;
        //public DataTemplate HeaderTemplate {
        //    get {
        //        return this._headerTemplate;
        //    }
        //    set {
        //        this._headerTemplate = value;
        //        this.NotifyOfPropertyChange(() => this.HeaderTemplate);
        //    }
        //}

        public Thickness Margin { get; set; }

        public bool IsNotifying {
            get; set;
        }

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

            this.NotifyOfPropertyChange("Margin");
        }

        public void NotifyOfPropertyChange(string propertyName) {
            if (this.PropertyChanged != null)
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Refresh() {

        }
    }
}
