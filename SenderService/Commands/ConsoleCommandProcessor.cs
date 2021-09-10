using MessengerOptions.Factories;
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
                case CommandTypes.ChangeDatabaseLocation:
                    return ProcessChangeDatabaseLocation(command);
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

            using var context = new DbContextFactory().GetDbContext();

            foreach (var id in context.TelegramUsersInfo)
            {
                OutputService.Write($"{id.TelegramUsername} - {id.TelegramUserFirstName} {id.TelegramUserLastName} - ({id.TelegramUserId}) - {id.IsAdministrator}", true, false, null);
            }

            return match.Success;
        }

        public static bool ProcessDeleteUser(string command)
        {
            var match = Regex.Match(command, @"\s*delete\s*user\s*(\d*)\s*$", RegexOptions.IgnoreCase);
            if (!match.Success)
                return match.Success;

            using var context = new DbContextFactory().GetDbContext();

            var userId = Convert.ToInt64(match.Groups[1].Value);
            var user = context.TelegramUsersInfo.First(item => item.TelegramUserId == userId);

            if (user == null)
                return false;

            context.TelegramUsersInfo.Remove(user);

            return match.Success;
        }

        public static bool ProcessChangeDatabaseLocation(string command)
        {
            var match = Regex.Match(command, @"\s*change\s*database\s*location\s*(.*)\s*$", RegexOptions.IgnoreCase);
            if (!match.Success)
                return match.Success;

            var newLocation = match.Groups[1].Value.ToString();


            VariablesProvider.ProgramConstants.UsersDatabase.Location = newLocation;
            VariablesProvider.SaveConstants();

            DbContextFactory.ChangeDatabaseLocation(VariablesProvider.ProgramConstants.UsersDatabase.Location);


            return match.Success;
        }

    }
}
