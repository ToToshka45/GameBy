using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RatingService.Domain.Entities;
using RatingService.Domain.Entities.Ratings;

namespace RatingService.Infrastructure.DataAccess.Configurations.Entities.Ratings;

internal class ParticipantsRatingsConfiguration : IEntityTypeConfiguration<ParticipantRating>
{
    public void Configure(EntityTypeBuilder<ParticipantRating> builder)
    {
        builder.ToTable("participants_ratings");

        builder.Property(e => e.ExternalParticipantId).HasColumnName("external_participant_id");
        builder.HasOne<Participant>().WithOne().HasForeignKey<ParticipantRating>(e => e.ExternalParticipantId).OnDelete(DeleteBehavior.Cascade);
    }
}
