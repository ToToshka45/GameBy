using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RatingService.Domain.Aggregates;
using RatingService.Domain.Entities.Ratings;

namespace RatingService.Infrastructure.DataAccess.Configurations.Entities.Ratings;

internal class GamersRatingsConfiguration : IEntityTypeConfiguration<GamerRating>
{
    public void Configure(EntityTypeBuilder<GamerRating> builder)
    {
        builder.ToTable("gamers_ratings");

        builder.Property(e => e.ExternalUserId).HasColumnName("external_user_id");
        builder.HasOne<UserInfo>().WithMany().HasForeignKey(ur => ur.ExternalUserId).OnDelete(DeleteBehavior.Cascade);
    }
}
