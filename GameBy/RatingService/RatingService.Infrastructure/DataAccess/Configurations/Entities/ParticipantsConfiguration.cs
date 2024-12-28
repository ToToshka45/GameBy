using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RatingService.Domain.Aggregates;
using RatingService.Domain.Entities;
namespace RatingService.Infrastructure.DataAccess.Configurations.Entities;

internal class ParticipantsConfiguration : IEntityTypeConfiguration<Participant>
{
    public void Configure(EntityTypeBuilder<Participant> builder)
    {
        builder.ToTable("participants");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedNever();

        builder.Property(e => e.ExternalParticipantId).HasColumnName("external_participant_id");
        builder.Property(p => p.ExternalEventId).HasColumnName("external_event_id");
        builder.Property(e => e.ExternalUserId).HasColumnName("external_user_id");
        builder.Property(P => P.ParticipationState).HasColumnName("participation_state").HasConversion<string>();

        builder.HasOne(p => p.Rating).WithOne().HasForeignKey<Participant>(p => p.Id);
        builder.Navigation(e => e.Rating).AutoInclude();
        builder.HasOne<EventInfo>().WithMany().HasForeignKey(e => e.ExternalEventId);
        builder.HasOne<UserInfo>().WithMany().HasForeignKey(e => e.ExternalUserId);
    }
}
