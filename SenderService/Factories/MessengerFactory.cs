using MessengerOptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramMessenger.Variables;
using WhatsAppMessenger.Variables;

namespace SenderService.Factories
{
    public class MessengerFactory
    {
        private static IMessenger _telegramMessengerInstance;
        private static IMessenger _whatsAppMessengerInstance;

        public IMessenger GetTelegramMessenger(TelegramVariables variables = null)
        {
            return _telegramMessengerInstance ??= new TelegramMessenger.TelegramMessenger(variables);
        }
        public IMessenger GetWhatsAppMessenger(WhatsAppVariables variables = null)
        {
            return _whatsAppMessengerInstance ??= new WhatsAppMessenger.WhatsAppMessenger(variables);
        }
    }
}
