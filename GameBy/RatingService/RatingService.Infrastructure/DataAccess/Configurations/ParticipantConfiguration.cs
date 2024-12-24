using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RatingService.Domain.Entities;
using RatingService.Domain.Enums;

namespace RatingService.Infrastructure.DataAccess.Configurations;

internal class ParticipantConfiguration : IEntityTypeConfiguration<Participant>
{
    public void Configure(EntityTypeBuilder<Participant> builder)
    {
        builder.HasKey(e => e.Id);

        builder.ToTable("participants");

        builder.Property(e => e.ExternalParticipantId).HasColumnName("external_participant_id");
        builder.Property(e => e.UserId).HasColumnName("user_id");
        builder.Property(e => e.EventId).HasColumnName("event_id");

        builder.Property(P => P.ParticipationState).HasColumnName("participation_state").HasConversion<string>();

        builder.HasOne(p => p.Rating).WithOne().HasForeignKey<Participant>(p => p.Id);
    }
}
