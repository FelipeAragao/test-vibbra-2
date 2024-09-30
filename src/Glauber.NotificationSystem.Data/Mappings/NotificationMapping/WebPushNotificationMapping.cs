using Glauber.NotificationSystem.Business.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Glauber.NotificationSystem.Data.Mappings.NotificationMapping;

public class WebPushNotificationMapping : IEntityTypeConfiguration<WebPushNotification>
{
    public void Configure(EntityTypeBuilder<WebPushNotification> builder)
    {
        builder.HasKey(s => s.Id);

        builder.ToTable("WebPushNotification");
    }
}
