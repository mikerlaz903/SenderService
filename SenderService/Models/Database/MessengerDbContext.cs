using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenderService.Models.Database
{
    public partial class MessengerDbContext : DbContext
    {
        private readonly string _connectionString;
        public MessengerDbContext(string connectionString) : base()
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
                return;
            optionsBuilder.UseSqlServer(_connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsersInfo>()
                .HasKey(k => k.Id);
        }

        public virtual DbSet<UsersInfo> UsersInfo { get; set; }
    }
}
