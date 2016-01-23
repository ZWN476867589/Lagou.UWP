using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Lagou.UWP.Common {
    public class FlyoutBehavior : Behavior<Flyout> {

        public static readonly DependencyProperty IsClosedProperty =
            DependencyProperty.Register(
                "IsClosed",
                typeof(bool),
                typeof(FlyoutBehavior),
                new PropertyMetadata(false, Changed));

        public bool IsClosed {
            get {
                return (bool)this.GetValue(IsClosedProperty);
            }
            set {
                this.SetValue(IsClosedProperty, value);
            }
        }

        private static void Changed(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            if ((bool)e.NewValue)
                ((FlyoutBehavior)d).AssociatedObject.Hide();
        }

        protected override void OnAttached() {
            base.OnAttached();

            this.AssociatedObject.Opened += AssociatedObject_Opened;
            this.AssociatedObject.Closed += AssociatedObject_Closed;
        }

        protected override void OnDetaching() {
            base.OnDetaching();

            this.AssociatedObject.Opened -= AssociatedObject_Opened;
            this.AssociatedObject.Closed -= AssociatedObject_Closed;
        }

        private void AssociatedObject_Closed(object sender, object e) {
            this.SetValue(IsClosedProperty, true);
        }

        private void AssociatedObject_Opened(object sender, object e) {
            //var flyout = (Flyout)sender;
            //flyout.SetValue(IsClosedProperty, false);
            this.SetValue(IsClosedProperty, false);
        }

    }
}
