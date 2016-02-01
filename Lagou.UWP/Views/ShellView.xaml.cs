using Caliburn.Micro;
using Lagou.UWP.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace Lagou.UWP.Views {
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class ShellView : Page {
        public ShellView() {
            this.InitializeComponent();

            this.pivot.SelectionChanged += Pivot_SelectionChanged;
        }

        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            this.ChangeHeader(this.pivot);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            base.OnNavigatedTo(e);
            // 因为将 NavigationCacheMode 设为了 Enabled , 导至 TopHeader.Content 没有重新赋值
            // 至使头没有更新
            this.ChangeHeader(this.pivot);
        }

        public void ChangeHeader(Pivot p) {
            var model = p.SelectedItem;
            if (model != null) {
                var view = ViewLocator.LocateForModel(model, null, null);
                TopHeader.LastHeaderFrom = view;
            }
        }
    }
}
