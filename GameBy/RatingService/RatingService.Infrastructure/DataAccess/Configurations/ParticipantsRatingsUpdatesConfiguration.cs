using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RatingService.Domain.Entities;

namespace RatingService.Infrastructure.DataAccess.Configurations;

internal class ParticipantsRatingsUpdateConfiguration : IEntityTypeConfiguration<ParticipantRatingUpdate>
{
    public void Configure(EntityTypeBuilder<ParticipantRatingUpdate> builder)
    {
        builder.ToTable("participants_ratings_updates");
    }
}
