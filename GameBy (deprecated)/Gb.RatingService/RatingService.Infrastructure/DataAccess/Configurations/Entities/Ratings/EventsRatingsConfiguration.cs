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

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedNever();

        builder.Property(e => e.Value).HasColumnName("value").HasColumnType("decimal(3,2)");

        builder.Property(e => e.OrganizerRatingId).HasColumnName("organizer_rating_id");
        builder.Property(e => e.EventInfoId).HasColumnName("event_info_id");

        builder.HasOne<OrganizerRating>().WithMany(e => e.EventRatings).HasForeignKey(e => e.OrganizerRatingId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne<EventInfo>().WithOne(e => e.Rating).HasForeignKey<EventRating>(e => e.EventInfoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
