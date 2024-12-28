//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using RatingService.Domain.Aggregates;
//using RatingService.Domain.Entities;

//namespace RatingService.Infrastructure.DataAccess.Configurations;

//internal class UserRatingConfiguration : IEntityTypeConfiguration<UserRating>
//{
//    public void Configure(EntityTypeBuilder<UserRating> builder)
//    {
//        builder.ToTable("user_ratings");
//        builder.HasKey(e => e.Id);

//        builder.Property(ur => ur.Category).HasColumnName("category").HasConversion<string>();
//        builder.Property(e => e.Value).HasColumnName("value").HasColumnType("decimal(3,2)");

//        builder.HasOne<UserInfo>().WithMany().HasForeignKey(ur => ur.UserId).OnDelete(DeleteBehavior.Cascade);
//    }
//}
