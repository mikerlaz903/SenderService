using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenderService.Exceptions
{
    public class IncompatibleReceiveVersionException : Exception
    {
        public IncompatibleReceiveVersionException() : base() { }
        public IncompatibleReceiveVersionException(string message) : base(message) { }
        public IncompatibleReceiveVersionException(string message, Exception inner) : base(message, inner) { }

        protected IncompatibleReceiveVersionException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
