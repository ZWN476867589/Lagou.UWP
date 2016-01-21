using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagou.UWP.ViewModels {
    public abstract class BasePageVM : BaseVM {

        public abstract string Title { get; }

        /// <summary>
        /// 字体图标
        /// </summary>
        public abstract char Glyph { get; }


        public BasePageVM() {
            this.DisplayName = this.Title;
        }
    }
}
