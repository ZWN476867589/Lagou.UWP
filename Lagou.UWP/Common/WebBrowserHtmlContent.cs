using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Lagou.UWP.Common {
    public static class WebBrowserHtmlContent {

        public static readonly DependencyProperty HtmlProperty =
            DependencyProperty.RegisterAttached(
                "Html",
                typeof(string),
                typeof(WebBrowserHtmlContent),
                new PropertyMetadata("", HtmlChanged)
                );

        public static void SetHtml(FrameworkElement target, float value) {
            target.SetValue(HtmlProperty, value);
        }

        public static float GetHtml(FrameworkElement target) {
            return (float)target.GetValue(HtmlProperty);
        }

        private static void HtmlChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            if (!string.IsNullOrWhiteSpace((string)e.NewValue)) {
                var browser = (WebView)d;
                if (browser == null)
                    throw new NotSupportedException("WebBrowserHtmlContent 只支持 WebBrowser");

                browser.NavigateToString((string)e.NewValue);
            }
        }
    }
}
