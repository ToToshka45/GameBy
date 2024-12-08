using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RatingService.Domain.Entities;

namespace RatingService.Infrastructure.DataAccess.Configurations;

internal class EventRatingUpdateConfiguration : IEntityTypeConfiguration<EventRatingUpdate>
{
    public void Configure(EntityTypeBuilder<EventRatingUpdate> builder)
    {
        builder.HasKey(e => e.Id);

        builder.ToTable("events_rating_updates");

        //builder.ComplexProperty(e => e.AuthorId);
        //builder.ComplexProperty(e => e.ExternalEventId);

        builder.HasOne(e => e.Rating).WithOne().HasForeignKey<EventRatingUpdate>(er => er.Id);
    }
}
