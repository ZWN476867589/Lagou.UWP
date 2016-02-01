using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;

namespace Lagou.UWP.Common {

    public class TapGestureBehavior : GestureRecognizerBehavior {
        protected override GestureSettings Setting {
            get {
                return GestureSettings.Tap;
            }
        }

        protected override void Attach(UIElement associatedObject) {
            this.Gesture.Tapped += Tapped;
        }

        protected override void DeAttach(UIElement associatedObject) {
            this.Gesture.Tapped -= Tapped;
        }

        private void Tapped(GestureRecognizer sender, TappedEventArgs args) {
            if (this.Command != null && this.Command.CanExecute(this.CommandParameter)) {
                this.Command.Execute(this.CommandParameter);
            }
        }
    }
}
