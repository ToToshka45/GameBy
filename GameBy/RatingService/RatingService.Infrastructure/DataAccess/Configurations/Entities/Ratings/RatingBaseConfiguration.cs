using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RatingService.Domain.Entities.Ratings;

namespace RatingService.Infrastructure.DataAccess.Configurations.Entities.Ratings;

internal class RatingBaseConfiguration : IEntityTypeConfiguration<RatingBase>
{
    public void Configure(EntityTypeBuilder<RatingBase> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Value).HasColumnName("value").HasColumnType("decimal(3,2)");
        //builder.HasOne<EventInfo>().WithOne(e => e.Rating).HasForeignKey<EventRating>(e => e.EventId);
    }
}
