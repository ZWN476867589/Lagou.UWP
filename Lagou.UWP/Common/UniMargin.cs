using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Lagou.UWP.Common {
    public class UniMargin {

        public static readonly DependencyProperty MarginProperty
            = DependencyProperty.RegisterAttached(
                "Margin",
                typeof(Thickness),
                typeof(UniMargin),
             new PropertyMetadata(0, MarginChanged));

        public static void SetMargin(FrameworkElement target, Thickness value) {
            target.SetValue(MarginProperty, value);
        }

        public static Thickness GetMargin(FrameworkElement target) {
            return (Thickness)target.GetValue(MarginProperty);
        }

        private static void MarginChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var fe = (FrameworkElement)d;
            fe.Loaded += fe_Loaded;
            Update(fe);
        }

        static void fe_Loaded(object sender, RoutedEventArgs e) {
            Update((FrameworkElement)sender);
        }

        private static void Update(FrameworkElement target) {
            var cc = VisualTreeHelper.GetChildrenCount(target);
            var padding = GetMargin(target);
            for (var i = 0; i < cc; i++) {
                var child = (FrameworkElement)VisualTreeHelper.GetChild(target, i);
                //var binding = new Binding() {
                //    Source = target,
                //    Mode = BindingMode.OneWay,
                //    Path = new PropertyPath("UniPadding.Padding")
                //};

                //child.SetBinding(FrameworkElement.MarginProperty, binding);
                child.Margin = padding;
            }
        }
    }
}
