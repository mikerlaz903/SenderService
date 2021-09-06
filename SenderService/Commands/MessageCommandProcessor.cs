using SenderService.Administration;
using SenderService.Factories;
using SenderService.Messengers;
using SenderService.Models.Database;
using SenderService.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace SenderService.Commands
{
    internal static class MessageCommandProcessor
    {

        public static bool MatchStart(string message, Messenger messenger)
        {
            var match = Regex.Match(message, @"/start");
            if (match.Success)
                messenger.ReturningMessage = "Your request is being processed. Wait for the administrator to clarify your status.";

            return match.Success;
        }

        public static bool MatchAddNewUserRequest(string message, TelegramMessenger messenger)
        {
            var match = Regex.Match(message, @"/applyNewUsersRequest\s*(\d*)\s*(.*)$");
            if (!match.Success)
                return false;

            var userId = Convert.ToInt64(match.Groups[1].Value);
            var username = match.Groups[2].Value;

            var newUser = new UsersInfo()
            {
                TelegramUserId = userId,
                TelegramUserName = username
            };
            AllowedUsers.UserList.Add(newUser);

            messenger.ReturningMessage = "";

            var dbFactory = new DbContextFactory();
            using var context = dbFactory.GetDbContext();
            var adminChat = context.GetAdministrator().TelegramUserId;
            TelegramBotAdministration.ReplyKeyboardRemove(messenger, adminChat, $"New user {newUser.TelegramUserId} was successfully added!");

            messenger.SendMessage(userId.ToString(), "You have been given access to inquiries and newsletters!");

            return true;
        }

        public static bool MatchShowRole(string message, User sender, TelegramMessenger messenger)
        {
            var match = Regex.Match(message, @"/show_role");
            if (!match.Success)
                return match.Success;

            var dbFactory = new DbContextFactory();
            using var context = dbFactory.GetDbContext();

            var isAdmin = context.IsAdmin(sender.Id);
            var role = isAdmin ? "Administrator" : "User";
            messenger.ReturningMessage = $"Your role is {role}";
            return match.Success;
        }
    }
}
