using Glauber.NotificationSystem.Business.Entities;
using Microsoft.EntityFrameworkCore;

namespace Glauber.NotificationSystem.Data.Context;

public class NotificationSystemDbContext(DbContextOptions<NotificationSystemDbContext> options) : DbContext(options)
{
    public DbSet<App> Apps { get; set; }
    public DbSet<WebPushSettings> WebPushSettings { get; set; }
    public DbSet<EmailSettings> EmailSettings { get; set; }
    public DbSet<SMSSettings> SMSSettings { get; set; }
    public DbSet<WebPushNotification> WebPushNotifications { get; set; }
    public DbSet<EmailNotification> EmailNotifications { get; set; }
    public DbSet<SMSNotification> SMSNotifications { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("SendDate") != null))
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property("SendDate").CurrentValue = DateTime.Now;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Property("SendDate").IsModified = false;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
