using Glauber.NotificationSystem.Business.Entities.Email;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Glauber.NotificationSystem.Data.Mappings.SettingsMapping.Email;

public class SenderMapping : IEntityTypeConfiguration<Sender>
{
    public void Configure(EntityTypeBuilder<Sender> builder)
    {
        builder.HasKey(s => s.Id);

        builder
            .HasOne(s => s.EmailSettings)
            .WithOne(e => e.Sender);

        builder.ToTable("Sender");
    }
}
