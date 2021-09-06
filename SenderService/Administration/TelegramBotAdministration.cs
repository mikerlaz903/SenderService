using SenderService.Messengers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace SenderService.Administration
{
    internal static class TelegramBotAdministration
    {
        public static void SendTelegramPermission(long userId, string username, TelegramMessenger messenger, long administratorChatId, string text)
        {
            var buttons = new List<KeyboardButton>();

            var applyNewUsersRequest = new KeyboardButton($"/applyNewUsersRequest {userId} {username}");
            var denyNewUsersRequest = new KeyboardButton("/cancel");

            buttons.Add(applyNewUsersRequest);
            buttons.Add(denyNewUsersRequest);

            var markup = new ReplyKeyboardMarkup(buttons, true, true);


            var id = new ChatId(administratorChatId);

            messenger.SendTelegramMessage(id, text, markup);
        }

        public static void ReplyKeyboardRemove(TelegramMessenger messenger, long chatId, string message)
        {
            var replyKeyboardRemove = new ReplyKeyboardRemove();
            var id = new ChatId(chatId);
            messenger.SendTelegramMessage(id, message, replyKeyboardRemove);
        }
    }
}
