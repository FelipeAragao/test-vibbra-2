using Glauber.NotificationSystem.Business.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Glauber.NotificationSystem.Data.Mappings.SettingsMapping;

public class WebPushSettingsMapping : IEntityTypeConfiguration<WebPushSettings>
{
    public void Configure(EntityTypeBuilder<WebPushSettings> builder)
    {
        builder.HasKey(s => s.Id);

        builder.ToTable("WebPushSettings");
    }
}
