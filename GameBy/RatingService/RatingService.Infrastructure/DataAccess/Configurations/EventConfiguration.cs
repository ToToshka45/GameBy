using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RatingService.Domain.Aggregates;
using RatingService.Domain.Enums;

namespace RatingService.Infrastructure.DataAccess.Configurations;

internal class EventConfiguration : IEntityTypeConfiguration<EventInfo>
{
    public void Configure(EntityTypeBuilder<EventInfo> builder)
    {
        builder.ToTable("events_info");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Category).HasConversion<string>();
        builder.Property(e => e.State).HasConversion<string>();
        //builder.ComplexProperty(e => e.EventId);

        builder.HasOne(e => e.Rating).WithOne().HasForeignKey<EventInfo>(e => e.Id);
        builder.Navigation(e => e.Rating).AutoInclude();

        builder.HasMany(e => e.Feedbacks).WithOne().HasForeignKey(f => f.Id);
        builder.HasMany(e => e.Participants).WithOne().HasForeignKey(f => f.Id);

    }
}
