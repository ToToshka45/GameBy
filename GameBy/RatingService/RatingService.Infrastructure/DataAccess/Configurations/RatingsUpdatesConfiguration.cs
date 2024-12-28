using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RatingService.Domain.Entities;
using RatingService.Domain.Entities.Ratings;

namespace RatingService.Infrastructure.DataAccess.Configurations;

internal class RatingsUpdatesConfiguration : IEntityTypeConfiguration<RatingUpdate>
{
    public void Configure(EntityTypeBuilder<RatingUpdate> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Value).HasColumnName("value").HasColumnType("decimal(3,2)");
        builder.Property(e => e.ExternalEventId).HasColumnName("external_event_id");
        builder.Property(e => e.AuthorId).HasColumnName("author_id");
        builder.Property(e => e.CreationDate).HasColumnName("creation_date");
        builder.Property(e => e.UpdateDate).HasColumnName("update_date");
        //builder.Property(e => e.EntityType).HasColumnName("entity_type").HasConversion<string>();

        builder.Property(e => e.RatingId).HasColumnName("rating_id");
    }
}
