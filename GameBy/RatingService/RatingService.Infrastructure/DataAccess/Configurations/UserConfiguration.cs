using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RatingService.Domain.Aggregates;

namespace RatingService.Infrastructure.DataAccess.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<UserInfo>
{
    public void Configure(EntityTypeBuilder<UserInfo> builder)
    {
        builder.ToTable("users_info");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.ExternalUserId).HasColumnName("external_user_id");
        builder.Property(e => e.UserName).HasColumnName("username");

        //builder.ComplexProperty(e => e.ExternalUserId);

        builder.HasMany(e => e.GamerFeedbacks).WithOne().HasForeignKey(f => f.Id);
        builder.HasMany(e => e.OrganizerFeedbacks).WithOne().HasForeignKey(f => f.Id);
        builder.HasMany(e => e.RatingsByCategory).WithOne().HasForeignKey(f => f.Id);
    }
}
