﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RatingService.Domain.Aggregates;
using RatingService.Domain.Entities;

namespace RatingService.Infrastructure.DataAccess.Configurations.Entities;

internal class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
{
    public void Configure(EntityTypeBuilder<Feedback> builder)
    {
        builder.ToTable("feedbacks");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.ExternalEventId).HasColumnName("external_event_id");
        builder.Property(e => e.AuthorId).HasColumnName("author_id");
        builder.Property(c => c.Content).HasMaxLength(250).HasColumnName("content");
        builder.Property(c => c.CreationDate).HasColumnName("creation_date");
        builder.Property(c => c.UpdateDate).HasColumnName("update_date");

        builder.OwnsOne(f => f.Receiver, navBuilder =>
        {
            navBuilder.Property(r => r.EntityType).HasColumnName("entity_type").HasConversion<string>();
            navBuilder.Property(r => r.SubjectId).HasColumnName("subject_id");
        });

        builder.HasOne<EventInfo>().WithMany().HasForeignKey(e => e.ExternalEventId);

    }
}
