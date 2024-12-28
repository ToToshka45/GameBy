using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RatingService.Domain.Aggregates;
using RatingService.Domain.Entities.Ratings;

namespace RatingService.Infrastructure.DataAccess.Configurations.Entities.Ratings;

internal class OrganizersRatingsConfiguration : IEntityTypeConfiguration<OrganizerRating>
{
    public void Configure(EntityTypeBuilder<OrganizerRating> builder)
    {
        builder.ToTable("organizers_ratings");

        builder.Property(e => e.ExternalUserId).HasColumnName("external_user_id");
        builder.HasOne<UserInfo>().WithMany().HasForeignKey(ur => ur.ExternalUserId).OnDelete(DeleteBehavior.Cascade);
    }
}
