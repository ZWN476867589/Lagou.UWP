using Caliburn.Micro;
using Lagou.API;
using Lagou.API.Entities;
using Lagou.API.Methods;
using Lagou.UWP.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagou.UWP.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class CompanyViewModel : BasePageVM {
        public override char Glyph {
            get {
                throw new NotImplementedException();
            }
        }

        public override string Title {
            get {
                return "公司信息";
            }
        }

        private int? OldCompanyID = null;
        public int CompanyID {
            get; set;
        }

        public string CompanyName { get; set; }

        public string CompanyLogo { get; set; }

        public Company2 Data { get; set; }


        public ProductsViewModel ProductsVM { get; set; }

        public HistoryViewModel HistoryVM { get; set; }

        private SimpleContainer _container = null;

        public CompanyViewModel(SimpleContainer container) {
            this._container = container;
        }

        protected async override void OnActivate() {
            base.OnActivate();

            if (this.OldCompanyID != this.CompanyID || this.Data == null) {
                this.Data = null;

                await Task.Delay(500)
                    .ContinueWith(async t => {
                        await this.LoadData();
                    });
                this.OldCompanyID = this.CompanyID;
            }
        }

        private async Task LoadData() {
            this.IsBusy = true;

            var mth = new CompanyDetail() {
                CompanyID = this.CompanyID
            };

            this.Data = await ApiClient.Execute(mth);
            this.ProductsVM = new ProductsViewModel(this.Data?.Products);
            this.HistoryVM = new HistoryViewModel(this.Data?.History);
            this.NotifyOfPropertyChange(() => this.Data);
            this.NotifyOfPropertyChange(() => this.ProductsVM);
            this.NotifyOfPropertyChange(() => this.HistoryVM);

            this.IsBusy = false;
        }
    }
}
