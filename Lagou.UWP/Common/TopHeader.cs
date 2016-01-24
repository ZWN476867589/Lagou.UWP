using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Lagou.UWP.Common {
    public class TopHeader : DependencyObject {

        public static readonly DependencyProperty ContentTemplateProperty =
            DependencyProperty.RegisterAttached(
                "ContentTemplate",
                typeof(DataTemplate),
                typeof(TopHeader),
                new PropertyMetadata(null));

        public static DataTemplate GetContentTemplate(FrameworkElement d) {
            return d.GetValue(ContentTemplateProperty) as DataTemplate;
        }

        public static void SetContentTemplate(FrameworkElement d, DataTemplate v) {
            d.SetValue(ContentTemplateProperty, v);
        }
    }
}
