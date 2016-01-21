﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Lagou.API.Attributes {

    [AttributeUsage(AttributeTargets.Field)]
    public class ErrorTagAttribute : Attribute {

        public string Tag {
            get;
            private set;
        }

        public ErrorTagAttribute(string tag) {
            this.Tag = tag;
        }

    }
}
