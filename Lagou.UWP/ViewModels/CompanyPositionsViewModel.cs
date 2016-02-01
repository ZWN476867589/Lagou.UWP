using Caliburn.Micro;
using Lagou.API.Entities;
using Lagou.API.Methods;
using Lagou.UWP.Attributes;
using Lagou.UWP.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace Lagou.UWP.ViewModels {
    /// <summary>
    /// 公司职位列表
    /// </summary>
    [Regist(InstanceMode.Singleton)]
    public class CompanyPositionsViewModel : BasePageVM {
        public override string Title {
            get {
                return "职位列表";
            }
        }

        /// <summary>
        /// 职位类型列表
        /// </summary>
        public List<string> PositionTypes { get; set; } = Enum.GetNames(typeof(PositionTypes)).ToList();

        private string _selectedPositionType = null;
        public string SelectedPositionType {
            get {
                return this._selectedPositionType;
            }
            set {
                this._selectedPositionType = value;
                this.NotifyOfPropertyChange(() => this.SelectedPositionType);
                this.SetPosType(value);
            }
        }

        private int? OldCompanyID = null;
        public int CompanyID { get; set; }

        public string CompanyName { get; set; }

        public string CompanyLogo { get; set; }


        public BindableCollection<PositionBrief> Datas { get; set; } = new BindableCollection<PositionBrief>();

        public ICommand ShowPositionCmd { get; set; }

        public ICommand LoadMoreCmd { get; set; }

        public ICommand ShowCompanyCmd { get; set; }

        public override char Glyph {
            get {
                throw new NotImplementedException();
            }
        }

        private int Page = 1;

        private INavigationService NS = null;

        public CompanyPositionsViewModel(INavigationService ns) {
            this.NS = ns;

            this.ShowPositionCmd = new Command((arg) => {
                var e = (SelectionChangedEventArgs)arg;
                this.ShowPosition((PositionBrief)e.AddedItems.FirstOrDefault());
            });

            this.LoadMoreCmd = new Command(async () => {
                await this.LoadPosByType();
            });

            this.ShowCompanyCmd = new Command(() => {
                this.ShowCompany();
            });
        }

        protected async override void OnActivate() {
            if (this.OldCompanyID != this.CompanyID || this.Datas.Count == 0) {
                // Becase it's singletone, so need clear datas when it show.
                this.Datas.Clear();

                await Task.Delay(500)
                    .ContinueWith(t => {
                        this.SelectedPositionType = this.PositionTypes.First();
                    });
                this.OldCompanyID = this.CompanyID;
            }
        }

        private async Task LoadPosByType() {
            this.IsBusy = true;

            var method = new PositionList() {
                CompanyID = this.CompanyID,
                PositionType = (PositionTypes)Enum.Parse(typeof(PositionTypes), this.SelectedPositionType),
                Page = this.Page
            };
            var datas = await API.ApiClient.Execute(method);
            if (!method.HasError && datas.Count() > 0) {
                this.Page++;
                this.Datas.AddRange(datas);
            }

            this.IsBusy = false;
        }

        private async void SetPosType(string type) {
            this.Page = 1;
            this.Datas.Clear();
            await this.LoadPosByType();
        }

        public void ShowPosition(PositionBrief pos) {
            if (pos == null)
                return;

            this.NS
                .For<JobDetailViewModel>()
                .WithParam(p => p.ID, pos.PositionId)
                .Navigate();
        }

        private void ShowCompany() {
            this.NS.For<CompanyViewModel>()
                .WithParam(p => p.CompanyID, this.CompanyID)
                .WithParam(p => p.CompanyName, this.CompanyName)
                .WithParam(p => p.CompanyLogo, this.CompanyLogo)
                .Navigate();
        }
    }
}
