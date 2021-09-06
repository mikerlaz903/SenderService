using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenderService.Exceptions
{
    public class NotSupportedMessengerException : Exception
    {
        public NotSupportedMessengerException() : base()
        { }
        public NotSupportedMessengerException(string message) : base(message)
        { }
        public NotSupportedMessengerException(string message, Exception inner) : base(message, inner)
        { }

        protected NotSupportedMessengerException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context)
        { }
    }
}
