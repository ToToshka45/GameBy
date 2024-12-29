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

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedNever();

        builder.Property(e => e.Value).HasColumnName("value").HasColumnType("decimal(3,2)");

        builder.Property(e => e.GamerRatingId).HasColumnName("gamer_rating_id");
        builder.Property(e => e.ParticipantId).HasColumnName("participant_id");
        //builder.Property(e => e.ExternalParticipantId).HasColumnName("external_participant_id");

        builder.HasOne<GamerRating>().WithMany(e => e.ParticipantRatings).HasForeignKey(e => e.GamerRatingId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne<Participant>().WithOne(e => e.Rating).HasForeignKey<ParticipantRating>(e => e.ParticipantId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
