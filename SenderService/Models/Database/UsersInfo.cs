using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenderService.Models.Database
{
    public class UsersInfo
    {
        public UsersInfo()
        {

        }

        public int Id { get; set; }
        public string? TelegramUserName { get; set; }
        public long TelegramUserId { get; set; }
        public bool IsAdministrator { get; set; }
    }
}
