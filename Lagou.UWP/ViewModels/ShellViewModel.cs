using Caliburn.Micro;
using Lagou.UWP.Attributes;
using Lagou.UWP.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagou.UWP.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class ShellViewModel : Screen {

        private readonly IEventAggregator _eventAggregator;

        public BindableCollection<BaseVM> Datas { get; set; } = new BindableCollection<BaseVM>();

        public SearchBarViewModel SearchBarVM { get; set; } = new SearchBarViewModel();

        public ShellViewModel(SimpleContainer container, IEventAggregator eventAggregator) {
            this._eventAggregator = eventAggregator;

            this.Datas.Add(container.GetInstance<IndexViewModel>());
            this.Datas.Add(container.GetInstance<SearchViewModel>());
            this.Datas.Add(container.GetInstance<MyViewModel>());
            this.Datas.Add(container.GetInstance<LocalFavoriteViewModel>());
        }

        protected override void OnActivate() {
            _eventAggregator.Subscribe(this);
            base.OnActivate();
        }
    }
}
