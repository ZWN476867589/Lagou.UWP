using Caliburn.Micro;
using Lagou.API.Entities;
using Lagou.UWP.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagou.UWP.ViewModels {

    [Regist(InstanceMode.PreRequest)]
    public class SearchedItemViewModel : BaseVM {

        public PositionBrief Data {
            get; set;
        }

        public SearchedItemViewModel(PositionBrief data, INavigationService ns) {
            this.Data = data;
            //this.TapCmd = new Command(() => ShowDetail());
            //this.NS = ns;
        }

    }
}
