using Microsoft.EntityFrameworkCore;
using NotificationsTest.DataAccess.Models;

namespace NotificationsTest.DataAccess
{
    public class NotificationsContext : DbContext
    {
        public NotificationsContext(DbContextOptions<NotificationsContext> options)
            : base(options)
        {
        }

        public DbSet<Message> Messages { get; set; }

        public DbSet<MessageRecipient> MessageRecipients { get; set; }
    }
}
