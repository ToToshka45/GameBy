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

        builder.HasOne(e => e.Rating).WithOne().HasForeignKey<UserRatingUpdate>(ur => ur.Id);
    }
}
