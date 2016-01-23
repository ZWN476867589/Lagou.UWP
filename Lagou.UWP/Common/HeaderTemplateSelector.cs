using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Reflection;

namespace Lagou.UWP.Common {
    public class HeaderTemplateSelector : DataTemplateSelector {

        //public DataTemplate ContentTemplate { get; set; }

        public DataTemplate TitleTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container) {
            if (item == null) {
                return null;
            }

            if (typeof(UIElement).IsAssignableFrom(item.GetType())) {
                return null;
            } else {
                return this.TitleTemplate;
            }
        }
    }
}
