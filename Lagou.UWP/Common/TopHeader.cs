using Caliburn.Micro;
using Lagou.UWP.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace Lagou.UWP.Common {
    public class TopHeader {

        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.RegisterAttached(
                "Content",
                typeof(object),
                typeof(TopHeader),
                new PropertyMetadata(null, Changed));


        private static DependencyObject _lastHeaderFrom = null;
        public static DependencyObject LastHeaderFrom {
            get {
                return _lastHeaderFrom;
            }
            set {
                if (value != _lastHeaderFrom) {
                    _lastHeaderFrom = value;
                    RootFrameViewModel.Instance.Value.Header =
                        value.GetValue(ContentProperty);
                }
            }
        }

        private static void Changed(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            //RootFrameViewModel.Instance.Value.Header = e.NewValue;
            LastHeaderFrom = d;
        }

        public static object GetContent(FrameworkElement d) {
            return d.GetValue(ContentProperty);
        }

        public static void SetContent(FrameworkElement d, object v) {
            d.SetValue(ContentProperty, v);
        }
    }
}
