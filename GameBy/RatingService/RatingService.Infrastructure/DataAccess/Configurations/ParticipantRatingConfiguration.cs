using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RatingService.Domain.Entities;

namespace RatingService.Infrastructure.DataAccess.Configurations;

internal class ParticipantRatingConfiguration : IEntityTypeConfiguration<ParticipantRating>
{
    public void Configure(EntityTypeBuilder<ParticipantRating> builder)
    {
        builder.ToTable("participants_ratings");
        builder.HasKey(e => e.Id);

        builder.Property(er => er.Category).HasConversion<string>();
    }
}
