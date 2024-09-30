using Glauber.NotificationSystem.Business.Entities.Email;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Glauber.NotificationSystem.Data.Mappings.SettingsMapping.Email;

public class EmailTemplateMapping : IEntityTypeConfiguration<EmailTemplate>
{
    public void Configure(EntityTypeBuilder<EmailTemplate> builder)
    {
        builder.HasKey(et => et.Id);

        builder.HasOne(et => et.EmailSettings)
            .WithMany(e => e.EmailTemplates)
            .HasForeignKey(et => et.EmailSettingsId);

        builder.ToTable("EmailTemplates");
    }
}
