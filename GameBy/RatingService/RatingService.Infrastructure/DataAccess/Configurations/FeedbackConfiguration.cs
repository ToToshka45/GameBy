﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RatingService.Domain.Entities;

namespace RatingService.Infrastructure.DataAccess.Configurations;

internal class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
{
    public void Configure(EntityTypeBuilder<Feedback> builder)
    {
        builder.HasKey(e => e.Id);

        builder.ToTable("feedbacks");

        builder.OwnsOne(f => f.Receiver);
        builder.OwnsOne(f => f.Content).Property(c => c.Content).HasMaxLength(250);
    }
}