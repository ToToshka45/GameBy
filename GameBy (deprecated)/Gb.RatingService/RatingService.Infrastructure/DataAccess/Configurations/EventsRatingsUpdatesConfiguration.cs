using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RatingService.Domain.Entities;
using RatingService.Domain.Entities.Ratings;

namespace RatingService.Infrastructure.DataAccess.Configurations;

internal class EventsRatingsUpdatesConfiguration : IEntityTypeConfiguration<EventRatingUpdate>
{
    public void Configure(EntityTypeBuilder<EventRatingUpdate> builder)
    {
        builder.ToTable("events_ratings_updates");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Value).HasColumnName("value").HasColumnType("decimal(3,2)");
        builder.Property(e => e.AuthorId).HasColumnName("author_id");
        builder.Property(e => e.CreationDate).HasColumnName("creation_date");
        builder.Property(e => e.UpdateDate).HasColumnName("update_date");
        builder.Property(e => e.SubjectId).HasColumnName("subject_id");
        builder.Property(e => e.RatingId).HasColumnName("rating_id");

        builder.HasOne<EventRating>().WithMany(e => e.Updates).HasForeignKey(e => e.RatingId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
