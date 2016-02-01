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
using Windows.UI.Xaml;

namespace Lagou.UWP.ViewModels {

    [Regist(InstanceMode.None)]
    public class SearchedItemViewModel : BaseVM {

        public PositionBrief Data {
            get; set;
        }

        public ICommand TapCmd { get; set; }

        private INavigationService NS;

        public SearchedItemViewModel(PositionBrief data, INavigationService ns) {
            this.Data = data;
            this.TapCmd = new Command(() => ShowDetail());
            this.NS = ns;
        }

        private void ShowDetail() {
            this.NS
                .For<JobDetailViewModel>()
                .WithParam(p => p.ID, this.Data.PositionId)
                //.WithParam(p => p.ID, 1178538)
                .Navigate();
        }
    }
}
