using Caliburn.Micro;
using Lagou.API.Entities;
using Lagou.UWP.Attributes;
using Lagou.UWP.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Universal.UI.Xaml.Controls;
using Windows.ApplicationModel.Core;
using Windows.UI.Popups;

namespace Lagou.UWP.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class LocalFavoriteViewModel : BasePageVM, IHandle<Position> {

        private static readonly string FAVORITE_FILE = "datas/Favorites.json";

        public override string Title {
            get {
                return "本地收藏";
            }
        }

        public override char Glyph {
            get {
                return (char)0xf005;
            }
        }

        private List<Position> Favorites = new List<Position>();


        public BindableCollection<SearchedItemViewModel> Datas { get; set; }

        public ICommand SwipCmd { get; set; }

        private INavigationService NS = null;
        private IEventAggregator EVA = null;

        public LocalFavoriteViewModel(SimpleContainer container, INavigationService ns, IEventAggregator eva) {
            this.NS = ns;
            this.EVA = eva;
            eva.Subscribe(this);

            this.SwipCmd = new Command((args) => {
                var e = (ItemSwipeEventArgs)args;
                
                if (e.Direction == SwipeListDirection.Right)
                    this.Remove(e.SwipedItem);
            });

            this.LoadData();
        }

        private async void LoadData() {
            this.Favorites = await FileManager.Instance.Value.ReadFromJson<List<Position>>(FAVORITE_FILE) ?? new List<Position>();
            //Properties.Get<List<Position>>(PropertiesKeys.Favorites.ToString()) ?? new List<Position>();
            this.Datas = new BindableCollection<SearchedItemViewModel>();
            var datas = this.Favorites.Select(f => {
                var d = this.Convert(f);
                return new SearchedItemViewModel(d, this.NS);
            });

            this.Datas.AddRange(datas);
        }

        private PositionBrief Convert(Position f) {
            return new PositionBrief() {
                City = f.WorkAddress,
                CompanyId = f.CompanyID,
                CompanyLogo = f.CompanyLogo,
                CompanyName = f.CompanyName,
                CreateTime = "N/A",
                PositionFirstType = "N/A",
                PositionId = f.PositionID,
                PositionName = f.JobTitle,
                Salary = f.Salary
            };
        }


        public async void Handle(Position arg) {
            var tip = "";
            if (!this.Favorites.Any(f => f.PositionID == arg.PositionID)) {
                var d = this.Convert(arg);
                this.Datas.Add(new SearchedItemViewModel(d, this.NS));
                this.AddToFavorite(arg);

                tip = "已收藏到本地";
            } else
                tip = "该职位已收藏";


            await DispatcherHelper.Run(async () => {
                var dialog = new MessageDialog(tip, "提示");
                await dialog.ShowAsync();
            });
        }

        private void AddToFavorite(Position data) {
            this.Favorites.Add(data);
            this.SaveFavorite();
        }

        private async void SaveFavorite() {
            //Properties.SetObject(PropertiesKeys.Favorites.ToString(), this.Favorites);
            await FileManager.Instance.Value.SaveAsJson(this.Favorites, FAVORITE_FILE);
        }


        private void Remove(object item) {
            var model = (SearchedItemViewModel)item;
            this.Datas.Remove(model);
            this.Favorites.RemoveAll(p => p.PositionID == model.Data.PositionId);
            this.SaveFavorite();
        }
    }
}
