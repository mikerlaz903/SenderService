using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenderService.Events
{
    internal class AddableListArgs<T> : EventArgs
    {
        public T NewValue { get; set; }
    }
}
