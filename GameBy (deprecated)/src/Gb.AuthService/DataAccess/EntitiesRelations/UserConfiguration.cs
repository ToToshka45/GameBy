using Domain;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntitiesRelations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder
                .OwnsOne(p => p.Login);
            builder
                .OwnsOne(p => p.Password);
            builder
                .OwnsOne(p => p.Email);
            builder.
                OwnsOne(p=>p.Phone);
        }
    }
}
