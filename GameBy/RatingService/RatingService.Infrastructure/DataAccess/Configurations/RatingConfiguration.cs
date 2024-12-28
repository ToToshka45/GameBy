using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RatingService.Domain.Entities;

namespace RatingService.Infrastructure.DataAccess.Configurations;

internal class RatingConfiguration : IEntityTypeConfiguration<Rating>
{
    public void Configure(EntityTypeBuilder<Rating> builder)
    {
        builder.ToTable("ratings");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.EntityType).HasColumnName("entity_type");
        builder.Property(e => e.SubjectId).HasColumnName("subject_id");
        builder.Property(e => e.Value).HasColumnName("value").HasColumnType("decimal(3,2)");
        //builder.HasOne<EventInfo>().WithOne(e => e.Rating).HasForeignKey<EventRating>(e => e.EventId);
    }
}
