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

        private static readonly FieldInfo FrameProperty = null;

        static NavigationHelper() {
            FrameProperty = typeof(FrameAdapter).GetField("frame", BindingFlags.Instance | BindingFlags.NonPublic);
        }

        public static void GoBack(this INavigationService ns, int count) {
            if (count <= 0)
                throw new ArgumentOutOfRangeException("count 必须大于0");

            //if (count == 1)
            //    ns.GoBack();

            var frame = (Frame)FrameProperty.GetValue(ns);
            if (frame.BackStackDepth >= count) {
                var pageStack = frame.BackStack[frame.BackStack.Count - count];
                ns.Navigate(pageStack.SourcePageType, pageStack.Parameter);
            }
        }

        public static void RemoveCurrentFromBackStack(this INavigationService ns) {
            var frame = (Frame)FrameProperty.GetValue(ns);
            if (frame.BackStackDepth > 1)
                frame.BackStack.RemoveAt(frame.BackStackDepth - 1);
        }
    }
}
