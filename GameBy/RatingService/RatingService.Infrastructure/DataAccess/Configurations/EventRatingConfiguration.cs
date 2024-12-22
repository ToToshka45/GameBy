using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RatingService.Domain.Entities;

namespace RatingService.Infrastructure.DataAccess.Configurations;

internal class EventRatingConfiguration : IEntityTypeConfiguration<EventRating>
{
    public void Configure(EntityTypeBuilder<EventRating> builder)
    {
        builder.ToTable("events_ratings");
        builder.HasKey(e => e.Id);
    }
}
