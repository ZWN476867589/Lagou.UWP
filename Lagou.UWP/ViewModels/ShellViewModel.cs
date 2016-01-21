using Caliburn.Micro;
using Lagou.UWP.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagou.UWP.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class ShellViewModel : BaseVM {
        public BindableCollection<BasePageVM> Datas { get; set; } = new BindableCollection<BasePageVM>();

        public ShellViewModel(SimpleContainer container) {
            this.Datas.Add(container.GetInstance<IndexViewModel>());
            this.Datas.Add(container.GetInstance<SearchViewModel>());
            this.Datas.Add(container.GetInstance<MyViewModel>());
            this.Datas.Add(container.GetInstance<LocalFavoriteViewModel>());
        }
    }
}
