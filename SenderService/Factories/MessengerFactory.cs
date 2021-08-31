using SenderService.Interfaces;
using SenderService.Messengers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenderService.Factories
{
    public class MessengerFactory
    {
        public ISender GetTelegramMessenger(string text = "")
        {
            return new TelegramMessenger(text);
        }
    }
}
