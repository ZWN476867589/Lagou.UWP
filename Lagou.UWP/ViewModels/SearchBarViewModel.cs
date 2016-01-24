using Caliburn.Micro;
using Lagou.UWP.Attributes;
using Lagou.UWP.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.ViewManagement;

namespace Lagou.UWP.ViewModels {

    [Regist(InstanceMode.PreRequest)]
    public class SearchBarViewModel : BaseVM {

        public CitySelectorViewModel CitySelectorVM { get; set; }

        public string City { get; set; } = "全国";

        public string Keyword { get; set; }

        private bool needCloseCitySelector = false;
        public bool NeedCloseCitySelector {
            get {
                return this.needCloseCitySelector;
            }
            set {
                this.needCloseCitySelector = value;
            }
        }

        public event EventHandler<SearchBarEventArgs> OnSubmit;

        public SearchBarViewModel(SimpleContainer container) {
            this.CitySelectorVM = container.GetInstance<CitySelectorViewModel>();
            this.CitySelectorVM.OnChoiced += CitySelectorVM_OnChoiced;
            this.CitySelectorVM.OnCancel += CitySelectorVM_OnCancel;
        }

        private void CitySelectorVM_OnCancel(object sender, EventArgs e) {
            this.NeedCloseCitySelector = true;
            this.NotifyOfPropertyChange(() => this.NeedCloseCitySelector);
        }

        private void CitySelectorVM_OnChoiced(object sender, CitySelectorViewModel.ChoicedCityEventArgs e) {
            this.City = e.City;
            this.NotifyOfPropertyChange(() => this.City);
        }

        public void Submit() {
            if (this.OnSubmit != null) {
                this.OnSubmit.Invoke(this, new SearchBarEventArgs() {
                    Keyword = this.Keyword,
                    City = this.City
                });
            }
            InputPane.GetForCurrentView().TryHide();
        }
    }

    public class SearchBarEventArgs : EventArgs {
        public string Keyword { get; set; }
        public string City { get; set; }
    }
}
