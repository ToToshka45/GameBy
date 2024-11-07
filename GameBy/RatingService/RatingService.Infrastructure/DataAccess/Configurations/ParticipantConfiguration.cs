using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RatingService.Domain.Models.Entities;

namespace RatingService.Infrastructure.DataAccess.Configurations;

internal class ParticipantConfiguration : IEntityTypeConfiguration<ParticipantInfo>
{
    public void Configure(EntityTypeBuilder<ParticipantInfo> builder)
    {
        builder.HasKey(e => e.)
    }
}
