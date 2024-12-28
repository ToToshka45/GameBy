using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RatingService.Domain.Entities;

namespace RatingService.Infrastructure.DataAccess.Configurations;

internal class EventsRatingsUpdatesConfiguration : IEntityTypeConfiguration<EventRatingUpdate>
{
    public void Configure(EntityTypeBuilder<EventRatingUpdate> builder)
    {
        builder.ToTable("events_ratings_updates");
    }
}
