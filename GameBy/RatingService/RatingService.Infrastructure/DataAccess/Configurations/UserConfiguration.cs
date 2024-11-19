using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RatingService.Domain.Aggregates;

namespace RatingService.Infrastructure.DataAccess.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<UserInfo>
{
    public void Configure(EntityTypeBuilder<UserInfo> builder)
    {
        builder.HasKey(e => e.Id);

        builder.ComplexProperty(e => e.UserId);
        builder.ComplexProperty(x => x.Rating);

        builder.HasMany(e => e.Feedbacks).WithOne().HasForeignKey(f => f.Id);
        builder.HasMany(e => e.Participants).WithOne().HasForeignKey(f => f.Id);

    }
}
