using Glauber.NotificationSystem.Business.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Glauber.NotificationSystem.Data.Mappings;

public class ActiveChannelsMapping : IEntityTypeConfiguration<ActiveChannels>
{
    public void Configure(EntityTypeBuilder<ActiveChannels> builder)
    {
        builder.HasKey(ac => ac.Id);

        builder.ToTable("ActiveChannels");
    }
}
