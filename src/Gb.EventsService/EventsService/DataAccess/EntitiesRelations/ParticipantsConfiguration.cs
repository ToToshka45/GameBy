using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntitiesRelations;

public class ParticipantsConfiguration : IEntityTypeConfiguration<Participant>
{
    public void Configure(EntityTypeBuilder<Participant> builder)
    {
        // Configure primary key
        builder.HasKey(e => e.Id);
        builder.ToTable("participants");

        // Configure relationships
        builder.HasOne<Event>()
               .WithMany()
               .HasForeignKey(p => p.EventId);

        builder.Property(e => e.State)
            .HasConversion<string>();
    }
}
