using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RatingService.Domain.Entities;

namespace RatingService.Infrastructure.DataAccess.Configurations;

internal class EventRatingUpdateConfiguration : IEntityTypeConfiguration<EventRatingUpdate>
{
    public void Configure(EntityTypeBuilder<EventRatingUpdate> builder)
    {
        builder.HasKey(e => e.Id);

        builder.ToTable("event_rating_updates");

        builder.Property(e => e.Value).HasColumnName("value").HasColumnType("decimal(3,2)");
        builder.Property(e => e.EventId).HasColumnName("event_id");
        builder.Property(e => e.AuthorId).HasColumnName("author_id");
        builder.Property(e => e.CreationDate).HasColumnName("creation_date");
        builder.Property(e => e.RatingId).HasColumnName("rating_id");

        //builder.ComplexProperty(e => e.AuthorId);
        //builder.ComplexProperty(e => e.ExternalEventId);

        builder.HasOne<EventRating>().WithMany().HasForeignKey(er => er.RatingId);
    }
}
