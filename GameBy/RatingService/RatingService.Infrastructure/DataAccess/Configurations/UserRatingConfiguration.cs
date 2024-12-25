using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RatingService.Domain.Aggregates;
using RatingService.Domain.Entities;

namespace RatingService.Infrastructure.DataAccess.Configurations;

internal class UserRatingConfiguration : IEntityTypeConfiguration<UserRating>
{
    public void Configure(EntityTypeBuilder<UserRating> builder)
    {
        builder.ToTable("users_ratings");
        builder.HasKey(e => e.Id);

        builder.Property(ur => ur.Category).HasColumnName("category").HasConversion<string>();

        builder.HasOne<UserInfo>().WithMany().HasForeignKey(ur => ur.UserId).OnDelete(DeleteBehavior.Cascade);
    }
}
