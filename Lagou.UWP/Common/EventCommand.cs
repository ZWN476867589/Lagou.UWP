﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using System.Reflection;
using Windows.Foundation;
using System.Runtime.InteropServices.WindowsRuntime;

namespace Lagou.UWP.Common {
    public class EventCommand {

        public static readonly DependencyProperty EventProperty =
            DependencyProperty.RegisterAttached(
                "Event",
                typeof(string),
                typeof(EventCommand),
                new PropertyMetadata(null));

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached(
                "Command",
                typeof(ICommand),
                typeof(EventCommand),
                new PropertyMetadata(null, CmdChanged));

        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.RegisterAttached(
                "CommandParameter",
                typeof(object),
                typeof(EventCommand),
                new PropertyMetadata(null, CmdChanged));

        public static void SetEvent(FrameworkElement target, string value) {
            target.SetValue(EventProperty, value);
        }

        public static string GetEvent(FrameworkElement target) {
            return (string)target.GetValue(EventProperty);
        }

        public static void SetCommand(FrameworkElement target, ICommand value) {
            target.SetValue(CommandProperty, value);
        }

        public static ICommand GetCommand(FrameworkElement target) {
            return (ICommand)target.GetValue(CommandProperty);
        }

        public static void SetCommandParameter(FrameworkElement target, object value) {
            target.SetValue(CommandParameterProperty, value);
        }

        public static object GetCommandParameter(FrameworkElement target) {
            return (object)target.GetValue(CommandParameterProperty);
        }

        private static void CmdChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var t = d.GetType();
            var evtName = GetEvent((FrameworkElement)d);
            var evt = t.GetEvent(evtName);

            if (evt != null) {
                //Adding or removing event handlers dynamically is not supported on WinRT events.
                //evt.AddEventHandler(d, new TypedEventHandler<DependencyObject, object>(EventDelegate));

                var addMethod = evt.AddMethod;
                var removeMethod = evt.RemoveMethod;
                var addParameters = addMethod.GetParameters();
                var delegateType = addParameters[0].ParameterType;
                Action<object, object> handler = (sender, args) => {
                    ////sender maybe not attached objected.
                    //FireCommand(sender, args as RoutedEventArgs);

                    FireCommand(d);
                };
                var invoke = typeof(Action<object, object>).GetRuntimeMethod("Invoke", new[] { typeof(object), typeof(object) });
                var @delegate = invoke.CreateDelegate(delegateType, handler);

                Func<object, EventRegistrationToken> add = a => (EventRegistrationToken)addMethod.Invoke(d, new object[] { @delegate });
                Action<EventRegistrationToken> remove = tt => removeMethod.Invoke(d, new object[] { tt });

                WindowsRuntimeMarshal.AddEventHandler(add, remove, handler);
            }
        }

        //private static void FireCommand(object sender, RoutedEventArgs routedEventArgs) {
        //    var ele = (FrameworkElement)sender;
        //    var cmd = GetCommand(ele);
        //    var parm = GetCommandParameter(ele);
        //    if (cmd != null && cmd.CanExecute(parm)) {
        //        cmd.Execute(parm);
        //    }
        //}

        private static void FireCommand(DependencyObject d) {
            var ele = (FrameworkElement)d;
            var cmd = GetCommand(ele);
            var parm = GetCommandParameter(ele);
            if (cmd != null && cmd.CanExecute(parm)) {
                cmd.Execute(parm);
            }
        }
    }
}
