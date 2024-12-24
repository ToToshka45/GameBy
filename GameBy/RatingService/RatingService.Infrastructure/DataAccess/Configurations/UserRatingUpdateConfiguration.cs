using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RatingService.Domain.Entities;

namespace RatingService.Infrastructure.DataAccess.Configurations;

internal class UserRatingUpdateConfiguration : IEntityTypeConfiguration<UserRatingUpdate>
{
    public void Configure(EntityTypeBuilder<UserRatingUpdate> builder)
    {
        builder.HasKey(e => e.Id);

        builder.ToTable("users_rating_updates");

        builder.Property(e => e.EventId).HasColumnName("event_id");
        builder.Property(e => e.AuthorId).HasColumnName("author_id");
        builder.Property(e => e.RatingOwnerId).HasColumnName("rating_owner_id");
        builder.Property(e => e.CreationDate).HasColumnName("creation_date");

        builder.HasOne(e => e.Rating).WithOne().HasForeignKey<UserRatingUpdate>(ur => ur.Id);
    }
}
