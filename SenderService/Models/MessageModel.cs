using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenderService.Models
{
    internal class MessageModel
    {
        public string MessageVersion { get; set; }
        public string Messenger { get; set; }
        public string UserIdHash { get; set; }
        public string Text { get; set; }
    }
}
