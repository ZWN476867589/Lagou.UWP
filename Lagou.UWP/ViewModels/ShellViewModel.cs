using Caliburn.Micro;
using Lagou.UWP.Attributes;
using Lagou.UWP.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace Lagou.UWP.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class ShellViewModel : Screen {

        private readonly IEventAggregator _eventAggregator;

        public BindableCollection<BaseVM> Datas { get; set; } = new BindableCollection<BaseVM>();

        public ShellViewModel(SimpleContainer container, IEventAggregator eventAggregator) {
            this._eventAggregator = eventAggregator;

            this.Datas.CollectionChanged += Datas_CollectionChanged;

            this.Datas.Add(container.GetInstance<IndexViewModel>());
            this.Datas.Add(container.GetInstance<DiscoverViewModel>());
            this.Datas.Add(container.GetInstance<MyViewModel>());
            this.Datas.Add(container.GetInstance<LocalFavoriteViewModel>());
        }

        private void Datas_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) {
            if (e.NewItems != null)
                foreach (var i in e.NewItems) {
                    var activator = (IActivate)i;
                    if (activator != null && !activator.IsActive)
                        activator.Activate();
                }

            if (e.OldItems != null)
                foreach (var i in e.OldItems) {
                    var activator = (IDeactivate)i;
                    if (activator != null)
                        activator.Deactivate(false);
                }
        }

        protected override void OnActivate() {
            //_eventAggregator.Subscribe(this);
            base.OnActivate();
        }
    }
}
