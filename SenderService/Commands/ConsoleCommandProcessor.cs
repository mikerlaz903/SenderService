using SenderService.Administration;
using SenderService.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SenderService.Commands
{
    internal static class ConsoleCommandProcessor
    {
        public static bool ProcessConsoleCommand(string command, CommandTypes commandType)
        {
            switch (commandType)
            {
                case CommandTypes.Help:
                    return ProcessHelp(command);
                case CommandTypes.ShowUsers:
                    return ProcessShowUsers(command);
                default:
                    return false;
            }
        }


        public static bool ProcessHelp(string command)
        {
            var match = Regex.Match(command, @"\s*help\s*$", RegexOptions.IgnoreCase);
            if (!match.Success)
                return match.Success;

            OutputService.Write("Command: " + Environment.NewLine +
                                Environment.NewLine +
                                "show users - to add new allowed user " + Environment.NewLine +
                                "exit - to exit", true, false, null);

            return match.Success;
        }

        public static bool ProcessShowUsers(string command)
        {
            var match = Regex.Match(command, @"\s*show\s*users\s*$", RegexOptions.IgnoreCase);
            if (!match.Success)
                return match.Success;

            foreach (var id in AllowedUsers.UserList)
            {
                OutputService.Write($"{id.TelegramUserName} ({id.TelegramUserId}) - {id.IsAdministrator}", true, false, null);
            }

            return match.Success;
        }

        public static bool ProcessDeleteUser(string command)
        {
            var match = Regex.Match(command, @"\s*delete\s*user\s*(\d*)\s*$", RegexOptions.IgnoreCase);
            if (!match.Success)
                return match.Success;

            var userId = Convert.ToInt64(match.Groups[1].Value);
            var user = AllowedUsers.UserList.Find(item => item.TelegramUserId == userId);

            if (user == null)
                return false;

            AllowedUsers.UserList.Remove(user);

            return match.Success;
        }

    }
}
