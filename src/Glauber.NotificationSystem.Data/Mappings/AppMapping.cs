using Glauber.NotificationSystem.Business.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Glauber.NotificationSystem.Data.Mappings;

public class AppMapping : IEntityTypeConfiguration<App>
{
    public void Configure(EntityTypeBuilder<App> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.AppName)
            .IsRequired()
            .HasColumnType("varchar(100)");

        builder.Property(a => a.AppToken)
            .IsRequired()
            .HasColumnType("varchar(30)");
        
        builder.HasOne(a => a.ActiveChannels)
            .WithOne(ac => ac.App);

        builder.HasOne(a => a.EmailSettings)
            .WithOne(e => e.App);

        builder.HasOne(a => a.SMSSettings)
            .WithOne(s => s.App);

        builder.HasOne(a => a.WebPushSettings)
            .WithOne(w => w.App);

        builder.HasMany(a => a.EmailNotification)
            .WithOne(en => en.App)
            .HasForeignKey(en => en.AppId);

        builder.HasMany(a => a.SMSNotification)
            .WithOne(en => en.App)
            .HasForeignKey(en => en.AppId);

        builder.HasMany(a => a.WebPushNotification)
            .WithOne(en => en.App)
            .HasForeignKey(en => en.AppId);

        builder.ToTable("App");
    }
}
