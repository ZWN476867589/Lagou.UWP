using Caliburn.Micro;
using Lagou.UWP.Attributes;
using Lagou.UWP.Common;
using Lagou.UWP.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Lagou.UWP {

    public sealed partial class App {
        private WinRTContainer _container;
        private IEventAggregator _eventAggregator;

        public App() {
            InitializeComponent();
            this.UnhandledException += App_UnhandledException;
        }

        private void App_UnhandledException(object sender, UnhandledExceptionEventArgs e) {
            e.Handled = true;
        }

        protected override void Configure() {
            this._container = new WinRTContainer();
            this._container.RegisterWinRTServices();

            //_container
            //    .Singleton<ShellViewModel>();

            var types = this.GetType().GetTypeInfo().Assembly.DefinedTypes
                .Select(t => new { T = t, Mode = t.GetCustomAttribute<RegistAttribute>()?.Mode })
                .Where(o => o.Mode != null && o.Mode != InstanceMode.None);

            foreach (var t in types) {
                var type = t.T.AsType();
                if (t.Mode == InstanceMode.Singleton) {
                    _container.RegisterSingleton(type, null, type);
                } else if (t.Mode == InstanceMode.PreRequest) {
                    _container.RegisterPerRequest(type, null, type);
                }
            }

            this._eventAggregator = _container.GetInstance<IEventAggregator>();
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args) {
            this.DisplayRootViewFor<ShellViewModel>();

            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar")) {
                var statusBar = StatusBar.GetForCurrentView();
                statusBar.BackgroundColor = Color.FromArgb(0xff, 0x00, 0x97, 0xa7);//#0097A7
                statusBar.BackgroundOpacity = 1;
            }

            if (args.PreviousExecutionState == ApplicationExecutionState.Terminated) {
                _eventAggregator.PublishOnUIThread(new ResumeStateMessage());
            }
        }

        protected override void OnSuspending(object sender, SuspendingEventArgs e) {
            _eventAggregator.PublishOnUIThread(new SuspendStateMessage(e.SuspendingOperation));
        }

        protected override object GetInstance(Type service, string key) {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service) {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance) {
            _container.BuildUp(instance);
        }


        protected override void OnUnhandledException(object sender, UnhandledExceptionEventArgs e) {
            e.Handled = true;
        }
    }
}
