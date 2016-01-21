using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagou.UWP.ViewModels {
    public abstract class BaseVM : Screen {

        private bool _isBusy = false;
        public bool IsBusy {
            get {
                return this._isBusy;
            }
            set {
                this._isBusy = value;
                this.NotifyOfPropertyChange(() => this.IsBusy);
            }
        }
    }
}
