using Lagou.API.Entities;
using Lagou.UWP.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagou.UWP.ViewModels {

    [Regist(InstanceMode.None)]
    public class EvaluationViewModel : BaseVM {

        public Evaluation Data { get; set; }

        public EvaluationViewModel(Evaluation data) {
            this.Data = data;
        }

    }
}
