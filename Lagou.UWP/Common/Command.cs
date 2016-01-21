using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Lagou.UWP.Common {
    public sealed class Command : ICommand {


        public event EventHandler CanExecuteChanged;
        /// <summary>
        /// 需要手动触发属性改变事件
        /// </summary>
        public void RaiseCanExecuteChanged() {
            if (CanExecuteChanged != null) {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// 决定当前绑定的Command能否被执行
        /// true：可以被执行
        /// false：不能被执行
        /// </summary>
        /// <param name="parameter">不是必须的，可以依据情况来决定，或者重写一个对应的无参函数</param>
        /// <returns></returns>
        public bool CanExecute(object parameter) {
            return this.IsCanExecute == null ? true : this.IsCanExecute(parameter);
        }

        /// <summary>
        /// 用于执行对应的命令，只有在CanExecute可以返回true的情况下才可以被执行
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter) {
            try {
                this.Action(parameter);
            } catch (Exception ex) {

            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Action<Object> Action { get; set; }
        public Func<Object, bool> IsCanExecute { get; set; }

        /// <summary>
        /// 构造函数，用于初始化
        /// </summary>
        /// <param name="execute"></param>
        /// <param name="canExecute"></param>
        public Command(Action<Object> execute, Func<Object, bool> canExecute) {
            this.Action = execute;
            this.IsCanExecute = canExecute;
        }
    }
}
