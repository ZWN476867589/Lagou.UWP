using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagou.UWP.ViewModels {
    public class LeaderViewModel : BaseVM {

        public IEnumerable<API.Entities.Company2._Leader> Datas { get; set; }

        public LeaderViewModel(IEnumerable<API.Entities.Company2._Leader> datas) {
            this.Datas = datas;
        }

    }
}
