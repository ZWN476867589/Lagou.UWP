using Lagou.UWP.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagou.UWP.ViewModels {

    [Regist(InstanceMode.None)]
    public class ProductsViewModel : BaseVM {
        public IEnumerable<API.Entities.Company2._Product> Datas { get; set; }

        public ProductsViewModel(IEnumerable<API.Entities.Company2._Product> datas) {
            this.Datas = datas;
        }
    }
}
