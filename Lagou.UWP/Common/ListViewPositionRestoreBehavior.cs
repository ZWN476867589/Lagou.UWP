using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Reflection;
using System.Linq.Expressions;

namespace Lagou.UWP.Common {
    public class ListViewPositionRestoreBehavior : Behavior<ListView> {


        private static readonly DependencyProperty PersistedItemKeyProperty =
            DependencyProperty.Register(
                "PersistedItemKey",
                typeof(string),
                typeof(ListViewPositionRestoreBehavior),
                new PropertyMetadata(string.Empty));



        public static readonly DependencyProperty IdentityProperty =
            DependencyProperty.Register(
                "Identity",
                typeof(Func<object, string>),
                typeof(ListViewPositionRestoreBehavior),
                new PropertyMetadata(null));



        public string PersistedItemKey {
            get {
                return this.GetValue(PersistedItemKeyProperty) as string;
            }
            set {
                this.SetValue(PersistedItemKeyProperty, value);
            }
        }

        public Func<object, string> Identity {
            get {
                return this.GetValue(IdentityProperty) as Func<object, string>;
            }
            set {
                this.SetValue(IdentityProperty, value);
            }
        }

        public ListViewPositionRestoreBehavior() {

        }


        protected override void OnAttached() {
            base.OnAttached();

            this.AssociatedObject.Loaded += AssociatedObject_Loaded;
            this.AssociatedObject.Unloaded += AssociatedObject_Unloaded;
        }

        protected override void OnDetaching() {
            base.OnDetaching();

            this.AssociatedObject.Loaded -= AssociatedObject_Loaded;
            this.AssociatedObject.Unloaded -= AssociatedObject_Unloaded;
        }

        private async void AssociatedObject_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e) {
            var lst = (ListView)sender;
            var key = this.PersistedItemKey;
            if (!string.IsNullOrWhiteSpace(key)) {
                await ListViewPersistenceHelper.SetRelativeScrollPositionAsync(
                        (ListView)sender,
                        key,
                        this.GetItem);
            }
        }

        private void AssociatedObject_Unloaded(object sender, Windows.UI.Xaml.RoutedEventArgs e) {
            var lst = (ListView)sender;
            var key = ListViewPersistenceHelper.GetRelativeScrollPosition(
                        (ListView)sender,
                        this.GetKey
                        );
            this.PersistedItemKey = key;
        }

        private IAsyncOperation<object> GetItem(string key) {
            //return Task.Run(() => {

            //}).AsAsyncOperation();

            if (this.Identity != null) {
                var items = this.AssociatedObject.ItemsSource as IEnumerable<object>;
                if (items != null) {
                    var item = items.FirstOrDefault(o => this.Identity(o).Equals(key));
                    return Task.FromResult(item).AsAsyncOperation();
                }
            }
            return Task.FromResult<object>(null).AsAsyncOperation();
        }

        private string GetKey(object item) {
            if (this.Identity != null)
                return this.Identity.Invoke(item);
            else
                return string.Empty;
        }
    }
}
