using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RatingService.Domain.Aggregates;

namespace RatingService.Infrastructure.DataAccess.Configurations;

internal class EventConfiguration : IEntityTypeConfiguration<EventInfo>
{
    public void Configure(EntityTypeBuilder<EventInfo> builder)
    {
        builder.HasKey(e => e.Id);

        //builder.ComplexProperty(e => e.EventId);

        builder.HasOne(e => e.Rating).WithOne().HasForeignKey<EventInfo>(e => e.Id);
        builder.HasMany(e => e.Feedbacks).WithOne().HasForeignKey(f => f.Id);
        builder.HasMany(e => e.Participants).WithOne().HasForeignKey(f => f.Id);

    }
}
