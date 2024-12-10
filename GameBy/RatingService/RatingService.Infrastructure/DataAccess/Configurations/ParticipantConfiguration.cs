using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RatingService.Domain.Entities;

namespace RatingService.Infrastructure.DataAccess.Configurations;

internal class ParticipantConfiguration : IEntityTypeConfiguration<Participant>
{
    public void Configure(EntityTypeBuilder<Participant> builder)
    {
        builder.HasKey(e => e.Id);

        builder.ToTable("participants");

        builder.Property(P => P.ParticipationState).HasConversion<string>();
        builder.HasOne(p => p.Rating).WithOne().HasForeignKey<Participant>(p => p.Id);
    }
}
