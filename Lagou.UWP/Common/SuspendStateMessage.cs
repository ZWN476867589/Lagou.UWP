using System;
using Windows.ApplicationModel;

namespace Lagou.UWP.Common {
    public class SuspendStateMessage {
        public SuspendStateMessage(SuspendingOperation operation) {
            Operation = operation;
        }

        public SuspendingOperation Operation { get; }
    }
}
