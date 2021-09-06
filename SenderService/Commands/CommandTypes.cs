using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenderService.Commands
{
    internal enum CommandTypes : ushort
    {
        Help = 1,
        ShowUsers = 2,
    }
}
