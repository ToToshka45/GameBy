using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RatingService.Domain.Entities;

namespace RatingService.Infrastructure.DataAccess.Configurations;

internal class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
{
    public void Configure(EntityTypeBuilder<Feedback> builder)
    {
        builder.HasKey(e => e.Id);

        builder.ToTable("feedbacks");

        builder.Property(e => e.EventId).HasColumnName("event_id");
        builder.Property(e => e.AuthorId).HasColumnName("author_id");
        builder.Property(e => e.EventId).HasColumnName("event_id");
        builder.Property(c => c.Content).HasMaxLength(250).HasColumnName("content");
        builder.Property(c => c.CreationDate).HasColumnName("creation_date");
        builder.Property(c => c.UpdateDate).HasColumnName("update_date");

        builder.OwnsOne(f => f.Receiver).Property(r => r.EntityType).HasConversion<string>();
    }
}
