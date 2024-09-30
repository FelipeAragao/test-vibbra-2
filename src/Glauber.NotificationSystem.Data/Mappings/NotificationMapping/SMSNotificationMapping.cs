using Glauber.NotificationSystem.Business.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Glauber.NotificationSystem.Data.Mappings.NotificationMapping;

public class SMSNotificationMapping : IEntityTypeConfiguration<SMSNotification>
{
    public void Configure(EntityTypeBuilder<SMSNotification> builder)
    {
        builder.HasKey(s => s.Id);

        builder.ToTable("SMSNotification");
    }
}
