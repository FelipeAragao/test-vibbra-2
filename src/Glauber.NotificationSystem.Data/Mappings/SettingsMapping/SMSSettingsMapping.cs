using Glauber.NotificationSystem.Business.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Glauber.NotificationSystem.Data.Mappings.SettingsMapping;

public class SMSSettingsMapping : IEntityTypeConfiguration<SMSSettings>
{
    public void Configure(EntityTypeBuilder<SMSSettings> builder)
    {
        builder.HasKey(s => s.Id);

        builder.HasOne(s => s.SMSProvider)
            .WithOne(sp => sp.SMSSettings);

        builder.ToTable("SMSSettings");
    }
}