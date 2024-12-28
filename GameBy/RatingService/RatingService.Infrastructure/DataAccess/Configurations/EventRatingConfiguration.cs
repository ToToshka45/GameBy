//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using RatingService.Domain.Aggregates;
//using RatingService.Domain.Entities;

//namespace RatingService.Infrastructure.DataAccess.Configurations;

//internal class EventRatingConfiguration : IEntityTypeConfiguration<EventRating>
//{
//    public void Configure(EntityTypeBuilder<EventRating> builder)
//    {
//        builder.ToTable("event_ratings");
//        builder.HasKey(e => e.Id);

//        builder.Property(e => e.EventId).HasColumnName("event_id");
//        builder.Property(e => e.Value).HasColumnName("value").HasColumnType("decimal(3,2)");
//        //builder.HasOne<EventInfo>().WithOne(e => e.Rating).HasForeignKey<EventRating>(e => e.EventId);
//    }
//}
