using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Lagou.UWP.Common {
    public class BindingProxy : DependencyObject {

        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register(
                "Data",
                typeof(object),
                typeof(BindingProxy),
                new PropertyMetadata(null));

        public object Data {
            get {
                return (object)GetValue(DataProperty);
            }
            set {
                SetValue(DataProperty, value);
            }
        }
    }
}
