using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RatingService.Domain.Aggregates;

namespace RatingService.Infrastructure.DataAccess.Configurations.Aggregates;

internal class EventConfiguration : IEntityTypeConfiguration<EventInfo>
{
    public void Configure(EntityTypeBuilder<EventInfo> builder)
    {
        builder.ToTable("events_info");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.ExternalEventId).HasColumnName("external_event_id");
        builder.Property(e => e.Category).HasColumnName("category").HasConversion<string>();
        builder.Property(e => e.State).HasColumnName("state").HasConversion<string>();
        builder.Property(e => e.Title).HasColumnName("title");
        builder.Property(e => e.CreationDate).HasColumnName("creation_date");

        builder.HasOne(e => e.Rating).WithOne().HasForeignKey<EventInfo>(e => e.Id);
        builder.Navigation(e => e.Rating).AutoInclude();

        //builder.HasMany(e => e.Feedbacks).WithOne().HasForeignKey(f => f.Id);
        //builder.HasMany(e => e.Participants).WithOne().HasForeignKey(e => e.EventId);
        //builder.Navigation(e => e.Participants).AutoInclude();
    }
}
