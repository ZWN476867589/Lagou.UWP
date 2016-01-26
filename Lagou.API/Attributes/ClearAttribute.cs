using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagou.API.Attributes {

    public abstract class ClearAttribute : Attribute {

        /// <summary>
        /// ASC
        /// </summary>
        public int Order { get; set; }

        public abstract string Clear(string str);
    }
}
