using SenderService.Factories;
using SenderService.Messengers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace SenderService.Administration
{
    internal static class OutputService
    {
        public static void Write(string text, bool consoleOutput, bool telegramAdministratorOutput, ChatId id)
        {
            if (consoleOutput)
                Console.WriteLine(text);

            var factory = new MessengerFactory();
            if (telegramAdministratorOutput && id == null)
                return;
            if (telegramAdministratorOutput)
            {
                var sender = (TelegramMessenger)factory.GetTelegramMessenger();
                sender.SendMessage(id, text);
            }
        }
        public static void Write(string text, bool consoleOutput, bool telegramAdministratorOutput, long id)
        {
            var chatId = new ChatId(id);
            Write(text, consoleOutput, telegramAdministratorOutput, chatId);
        }
        public static void Write(object text, bool consoleOutput, bool telegramAdministratorOutput, ChatId id)
        {
            Write(text.ToString(), consoleOutput, telegramAdministratorOutput, id);
        }
        public static void Write(object text, bool consoleOutput, bool telegramAdministratorOutput, long id)
        {
            var chatId = new ChatId(id);
            Write(text.ToString(), consoleOutput, telegramAdministratorOutput, chatId);
        }
    }
}
