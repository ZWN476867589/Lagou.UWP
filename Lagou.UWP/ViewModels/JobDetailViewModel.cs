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

namespace Lagou.UWP.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class JobDetailViewModel : BasePageVM {
        private string _title = "职位详情";
        public override string Title {
            get {
                return this._title;
            }
        }



        public Position Data { get; set; }

        public BindableCollection<EvaluationViewModel> Evaluations {
            get; set;
        } = new BindableCollection<EvaluationViewModel>();

        public bool HasEvaluations { get; set; }

        public bool NotHaveEvaluations { get; set; }

        private int? OldID = null;

        private int id = 0;
        public int ID {
            get {
                return this.id;
            }
            set {
                if (value != this.id) {
                    this.id = value;
                    this.Data = null;
                    this.Evaluations.Clear();
                    this._title = "职位详情";
                    //When ID changed, clear exists data.
                    this.NotifyOfPropertyChange(() => this.Data);
                    this.NotifyOfPropertyChange(() => this.Evaluations);
                    this.NotifyOfPropertyChange(() => this.Title);
                }
            }
        }

        public ICommand SeeAllCmd { get; set; }

        public ICommand AddFavoriteCmd { get; set; }

        public ICommand ShowCompanyCmd { get; set; }

        public override char Glyph {
            get {
                return char.MinValue;
            }
        }

        private readonly INavigationService NS = null;

        private readonly IEventAggregator _eventAggregator;

        public JobDetailViewModel(INavigationService ns, IEventAggregator eventAggregator) {
            this.NS = ns;
            this._eventAggregator = eventAggregator;
            this._eventAggregator.Subscribe(this);

            this.SeeAllCmd = new Command(() =>
                this.SeeAll()
            );

            this.AddFavoriteCmd = new Command(async () => {
                await this.AddFavorite();
            });

            this.ShowCompanyCmd = new Command(() => {
                this.ShowCompany();
            });
        }


        //Whether Singleton or PreRequest, when back to this,
        // OnActivate will fire.
        protected async override void OnActivate() {
            base.OnActivate();
            if (this.OldID != this.ID || this.Data == null) {
                await Task.Delay(500).ContinueWith((t) => this.LoadData());
                this.OldID = this.ID;
            }
        }

        private async Task LoadData() {

            this.IsBusy = true;

            var mth = new PositionDetail() {
                PositionID = this.ID
            };
            this.Data = await API.ApiClient.Execute(mth);
            this.NotifyOfPropertyChange(() => this.Data);

            if (this.Data != null) {
                this._title = $"{Data.JobTitle} - {Data.CompanyName}";
                this.NotifyOfPropertyChange(() => this.Title);
            }

            var mth2 = new EvaluationList() {
                PositionID = this.ID
            };
            var evs = await API.ApiClient.Execute(mth2);
            this.Evaluations.AddRange(evs.Select(e => new EvaluationViewModel(e)));
            this.NotifyOfPropertyChange(() => this.Evaluations);

            this.HasEvaluations = this.Evaluations.Count > 0;
            this.NotHaveEvaluations = !this.HasEvaluations;
            this.NotifyOfPropertyChange(() => this.HasEvaluations);
            this.NotifyOfPropertyChange(() => this.NotHaveEvaluations);

            this.IsBusy = false;
        }

        private void SeeAll() {
            this.NS.For<CompanyPositionsViewModel>()
                .WithParam(p => p.CompanyID, this.Data?.CompanyID)
                .WithParam(p => p.CompanyName, this.Data?.CompanyName)
                .WithParam(p => p.CompanyLogo, this.Data?.CompanyLogo)
                .Navigate();
        }

        private void ShowCompany() {
            this.NS.For<CompanyViewModel>()
                .WithParam(p => p.CompanyID, this.Data?.CompanyID)
                .WithParam(p => p.CompanyName, this.Data?.CompanyName)
                .WithParam(p => p.CompanyLogo, this.Data?.CompanyLogo)
                .Navigate();
        }

        private async Task AddFavorite() {
            await this._eventAggregator.PublishOnUIThreadAsync(this.Data);
        }
    }
}
