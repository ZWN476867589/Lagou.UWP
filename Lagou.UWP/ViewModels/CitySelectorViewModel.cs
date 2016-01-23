using Caliburn.Micro;
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
    public class CitySelectorViewModel : BaseVM {

        public event EventHandler OnCancel = null;
        public event EventHandler<ChoicedCityEventArgs> OnChoiced = null;

        public Dictionary<char, List<City>> Datas {
            get;
            set;
        }

        public BindableCollection<City> Datas2 { get; set; }

        public ICommand CancelCmd {
            get; set;
        }

        public CitySelectorViewModel() {
            var datas = Cities.Items.GroupBy(i => i.PY.ToUpper()[0])
             .ToDictionary(g => g.Key, g => g.ToList());

            this.Datas = datas;
            this.Datas2 = new BindableCollection<City>(Cities.Items);

            this.CancelCmd = new Command(() => {
                if (this.OnCancel != null)
                    this.OnCancel.Invoke(this, EventArgs.Empty);
            });
        }

        public void Choice(City c) {
            if (c != null && this.OnChoiced != null) {
                this.OnChoiced.Invoke(this, new ChoicedCityEventArgs(c.Name));

                this.CancelCmd.Execute(null);
            }
        }

        public class ChoicedCityEventArgs : EventArgs {
            public string City { get; private set; }

            public ChoicedCityEventArgs(string city) {
                this.City = city;
            }
        }
    }
}
