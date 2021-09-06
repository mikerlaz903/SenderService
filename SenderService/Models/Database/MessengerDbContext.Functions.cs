using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenderService.Models.Database
{
    public partial class MessengerDbContext
    {
        public UsersInfo GetAdministrator()
        {
            return UsersInfo.First(user => user.IsAdministrator);
        }

        public bool IsAdmin(long id)
        {
            return UsersInfo.Any(user => user.IsAdministrator && user.TelegramUserId == id);
        }
    }
}
