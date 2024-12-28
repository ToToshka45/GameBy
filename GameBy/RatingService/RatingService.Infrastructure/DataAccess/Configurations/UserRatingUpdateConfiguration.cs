//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using RatingService.Domain.Entities;

//namespace RatingService.Infrastructure.DataAccess.Configurations;

//internal class UserRatingUpdateConfiguration : IEntityTypeConfiguration<UserRatingUpdate>
//{
//    public void Configure(EntityTypeBuilder<UserRatingUpdate> builder)
//    {
//        builder.HasKey(e => e.Id);

//        builder.ToTable("user_rating_updates");

//        builder.Property(e => e.Value).HasColumnName("value").HasColumnType("decimal(3,2)");
//        builder.Property(e => e.EventId).HasColumnName("event_id");
//        builder.Property(e => e.AuthorId).HasColumnName("author_id");
//        builder.Property(e => e.UserId).HasColumnName("rating_owner_id");
//        builder.Property(e => e.CreationDate).HasColumnName("creation_date");
//        builder.Property(e => e.RatingId).HasColumnName("rating_id");

//        builder.HasOne<UserRating>().WithMany().HasForeignKey(ur => ur.RatingId);
//    }
//}
