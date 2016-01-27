using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Lagou.UWP.Common {
    public class DebugListView : ListView {
        public int GetContainerCount { get; set; }
        public int PrepareContainerCount { get; set; }
        protected override Windows.UI.Xaml.DependencyObject GetContainerForItemOverride() {
            GetContainerCount++;
            return base.GetContainerForItemOverride();
        }

        protected override void PrepareContainerForItemOverride(Windows.UI.Xaml.DependencyObject element, object item) {
            PrepareContainerCount++;
            Debug.WriteLine($"{this.PrepareContainerCount} , {this.GetContainerCount}");
            base.PrepareContainerForItemOverride(element, item);
        }
    }
}
