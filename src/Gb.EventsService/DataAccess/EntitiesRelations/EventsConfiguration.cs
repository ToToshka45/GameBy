using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntitiesRelations;

public class EventsConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        // Configure primary key
        builder.HasKey(e => e.Id);

        // Configure relationships
        builder.HasMany(e => e.EventActions)
               .WithOne() // Assuming EventAction has no navigation back to Event
               .HasForeignKey(p => p.EventId); // Assuming foreign key in EventAction table

        builder.HasMany(e => e.Participants)
               .WithOne() // Assuming EventMember has no navigation back to Event
               .HasForeignKey(p => p.EventId);

        builder.Property(e => e.EventStatus)
            .HasConversion<string>();
    }
}
