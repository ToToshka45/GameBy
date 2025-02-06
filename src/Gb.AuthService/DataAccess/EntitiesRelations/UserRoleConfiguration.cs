using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntitiesRelations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder
                .HasKey(bc => new { bc.UserId, bc.RoleId });

            builder
                .HasOne(bc => bc.User)
                .WithMany(b => b.Roles)
                .HasForeignKey(bc => bc.UserId);
            builder
                .HasOne(bc => bc.Role)
                .WithMany()
                .HasForeignKey(bc => bc.RoleId);
        }
    }
}
