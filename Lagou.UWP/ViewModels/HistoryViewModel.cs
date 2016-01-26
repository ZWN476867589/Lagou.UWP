using Lagou.UWP.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagou.UWP.ViewModels {

    [Regist(InstanceMode.None)]
    public class HistoryViewModel : BaseVM {


        public IEnumerable<API.Entities.Company2._History> Datas { get; set; }

        public HistoryViewModel(IEnumerable<API.Entities.Company2._History> datas) {
            this.Datas = datas;
        }

    }
}
