using Glauber.NotificationSystem.Business.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Glauber.NotificationSystem.Data.Mappings.SettingsMapping;

public class EmailSettingsMapping : IEntityTypeConfiguration<EmailSettings>
{
    public void Configure(EntityTypeBuilder<EmailSettings> builder)
    {
        builder.HasKey(s => s.Id);

        builder.HasOne(s => s.Sender)
            .WithOne(se => se.EmailSettings);

        builder.HasOne(s => s.Server)
            .WithOne(sv => sv.EmailSettings);

        builder
            .HasMany(s => s.EmailTemplates)
            .WithOne(et => et.EmailSettings)
            .HasForeignKey(et => et.EmailSettingsId);

        builder.ToTable("EmailSettings");
    }
}
