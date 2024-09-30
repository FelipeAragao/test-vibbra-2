using Glauber.NotificationSystem.Business.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Glauber.NotificationSystem.Data.Mappings.NotificationMapping;

public class EmailNotificationMapping : IEntityTypeConfiguration<EmailNotification>
{
    public void Configure(EntityTypeBuilder<EmailNotification> builder)
    {
        builder.HasKey(s => s.Id);
        
        builder.ToTable("EmailNotification");
    }
}
