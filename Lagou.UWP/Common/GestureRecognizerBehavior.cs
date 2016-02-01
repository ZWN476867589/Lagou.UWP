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

/**
https://code.msdn.microsoft.com/windowsapps/WindowsPhone-81-Gesture-4ada6f4a/sourcecode?fileId=133078&pathId=695867453
*/

namespace Lagou.UWP.Common {

    public abstract class GestureRecognizerBehavior : Behavior<UIElement> {

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command",
                typeof(ICommand),
                typeof(GestureRecognizerBehavior),
                new PropertyMetadata(null));

        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register("CommandParameter",
                typeof(object),
                typeof(GestureRecognizerBehavior),
                new PropertyMetadata(null));

        public ICommand Command {
            get {
                return this.GetValue(CommandProperty) as ICommand;
            }
            set {
                this.SetValue(CommandProperty, value);
            }
        }

        public object CommandParameter {
            get {
                return this.GetValue(CommandParameterProperty);
            }
            set {
                this.SetValue(CommandParameterProperty, value);
            }
        }





        protected GestureRecognizer Gesture { get; private set; }

        protected abstract GestureSettings Setting { get; }

        public GestureRecognizerBehavior() {
            this.Gesture = new GestureRecognizer() {
                GestureSettings = this.Setting
            };
        }

        protected abstract void Attach(UIElement associatedObject);

        protected abstract void DeAttach(UIElement associatedObject);


        protected override void OnAttached() {
            base.OnAttached();

            this.AssociatedObject.PointerCanceled += OnPointerCanceled;
            this.AssociatedObject.PointerPressed += OnPointerPressed;
            this.AssociatedObject.PointerReleased += OnPointerReleased;
            this.AssociatedObject.PointerMoved += OnPointerMoved;

            this.Attach(this.AssociatedObject);
        }


        protected override void OnDetaching() {
            base.OnDetaching();

            this.AssociatedObject.PointerCanceled -= OnPointerCanceled;
            this.AssociatedObject.PointerPressed -= OnPointerPressed;
            this.AssociatedObject.PointerReleased -= OnPointerReleased;
            this.AssociatedObject.PointerMoved -= OnPointerMoved;

            this.DeAttach(this.AssociatedObject);
        }






        private void OnPointerMoved(object sender, PointerRoutedEventArgs e) {
            this.Gesture.ProcessMoveEvents(e.GetIntermediatePoints(this.AssociatedObject));
        }

        private void OnPointerReleased(object sender, PointerRoutedEventArgs e) {
            this.Gesture.ProcessUpEvent(e.GetCurrentPoint(this.AssociatedObject));
            e.Handled = true;
        }

        private void OnPointerPressed(object sender, PointerRoutedEventArgs e) {
            // Route teh events to the gesture recognizer 
            this.Gesture.ProcessDownEvent(e.GetCurrentPoint(this.AssociatedObject));
            // Set the pointer capture to the element being interacted with 
            this.AssociatedObject.CapturePointer(e.Pointer);
            // Mark the event handled to prevent execution of default handlers 
            e.Handled = true;
        }

        private void OnPointerCanceled(object sender, PointerRoutedEventArgs e) {
            this.Gesture.CompleteGesture();
            e.Handled = true;
        }
    }
}
