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

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedNever();

        builder.Property(e => e.Value).HasColumnName("value").HasColumnType("decimal(3,2)");
        builder.Property(e => e.UserInfoId).HasColumnName("user_info_id");
        //builder.Property(e => e.ExternalUserId).HasColumnName("external_user_id");

        builder.HasOne<UserInfo>().WithOne(e => e.OrganizerRating).HasForeignKey<OrganizerRating>(ur => ur.UserInfoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
