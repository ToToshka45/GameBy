using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RatingService.Domain.Aggregates;
using RatingService.Domain.Entities.Ratings;

namespace RatingService.Infrastructure.DataAccess.Configurations.Entities.Ratings;

internal class EventsRatingsConfiguration : IEntityTypeConfiguration<EventRating>
{
    public void Configure(EntityTypeBuilder<EventRating> builder)
    {
        builder.ToTable("events_ratings");

        builder.Property(e => e.ExternalEventId).HasColumnName("external_event_id");
        builder.HasOne<EventInfo>().WithOne().HasForeignKey<EventRating>(e => e.ExternalEventId).OnDelete(DeleteBehavior.Cascade);
    }
}
