using SenderService.Collections;
using SenderService.Events;
using SenderService.Factories;
using SenderService.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenderService.Variables
{
    internal static class AllowedUsers
    {
        public static AddableList<UsersInfo> UserList { get; set; } = new();

        static AllowedUsers()
        {
            UserList.OnAdd += SaveUser;
            UserList.OnRemove += RemoveUser;
            UserList.AddRange(ReadUsers());
        }
        private static void SaveUser(object? sender, EventArgs e)
        {
            var dbFactory = new DbContextFactory();
            using var context = dbFactory.GetDbContext();

            if (e is not AddableListArgs<UsersInfo> newItem)
                return;

            var telegramUserId = newItem.NewValue.TelegramUserId;
            var telegramUserName = newItem.NewValue.TelegramUserName;

            var newUser = new UsersInfo
            {
                TelegramUserId = telegramUserId,
                TelegramUserName = telegramUserName
            };
            context.UsersInfo.Add(newUser);

            // добавить проверку на количество сохранений
            context.SaveChanges();
        }

        private static void RemoveUser(object? sender, EventArgs e)
        {
            var dbFactory = new DbContextFactory();
            using var context = dbFactory.GetDbContext();

            if (e is not AddableListArgs<UsersInfo> newItem)
                return;

            context.UsersInfo.Remove(newItem.NewValue);

            // добавить проверку на количество сохранений
            context.SaveChanges();
        }

        private static IEnumerable<UsersInfo> ReadUsers()
        {
            var dbFactory = new DbContextFactory();
            using var context = dbFactory.GetDbContext();

            return context.UsersInfo.ToList();
        }


    }
}
