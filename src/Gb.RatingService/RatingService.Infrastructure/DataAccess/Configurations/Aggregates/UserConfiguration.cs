using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RatingService.Domain.Aggregates;

namespace RatingService.Infrastructure.DataAccess.Configurations.Aggregates;

internal class UserConfiguration : IEntityTypeConfiguration<UserInfo>
{
    public void Configure(EntityTypeBuilder<UserInfo> builder)
    {
        builder.ToTable("users_info");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedNever();

        //builder.Property(e => e.ExternalUserId).HasColumnName("external_user_id");
        builder.Property(e => e.UserName).HasColumnName("username");
        builder.Navigation(e => e.GamerRating).AutoInclude();
        builder.Navigation(e => e.OrganizerRating).AutoInclude();
    }
}
