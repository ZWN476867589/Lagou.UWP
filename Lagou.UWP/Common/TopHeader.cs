using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Lagou.UWP.Common {
    public class TopHeader {

        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.RegisterAttached(
                "Content",
                typeof(FrameworkElement),
                typeof(TopHeader),
                new PropertyMetadata(null));

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.RegisterAttached(
                "Title", 
                typeof(string), 
                typeof(TopHeader), 
                new PropertyMetadata(null));

        public static FrameworkElement GetContent(FrameworkElement d) {
            return d.GetValue(ContentProperty) as FrameworkElement;
        }

        public static void SetContent(FrameworkElement d, FrameworkElement v) {
            d.SetValue(ContentProperty, v);
        }

        public static string GetTitle(FrameworkElement d) {
            return d.GetValue(TitleProperty) as string;
        }

        public static void SetTitle(FrameworkElement d, string v) {
            d.SetValue(TitleProperty, v);
        }
    }
}
