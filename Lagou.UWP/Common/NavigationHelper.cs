using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Windows.UI.Xaml.Controls;

namespace Lagou.UWP.Common {
    public static class NavigationHelper {
        public static void GoBack(this INavigationService ns, int count) {
            if (count <= 0)
                throw new ArgumentOutOfRangeException("count 必须大于0");

            if (ns.BackStack.Count >= count) {
                var pageStack = ns.BackStack[ns.BackStack.Count - count];
                ns.Navigate(pageStack.SourcePageType, pageStack.Parameter);
            }
        }
    }
}
