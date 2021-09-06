using SenderService.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenderService.Factories
{
    internal class DbContextFactory
    {
        public string ConnectionString { get; set; } =
            "Data Source=(LocalDB)\\MSSQLLocalDB;" +
            "AttachDbFilename=\"Users.mdf\";" +
            "Integrated Security=True;" +
            "Connect Timeout=30";

        public MessengerDbContext GetDbContext()
        {
            return new(ConnectionString);
        }

    }
}
