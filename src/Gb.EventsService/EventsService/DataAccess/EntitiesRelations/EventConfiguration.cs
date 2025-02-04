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
    public class EventConfiguration:IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            // Configure primary key
            builder.HasKey(e => e.Id); 

            // Configure relationships
            builder.HasMany(e => e.EventActions)
                   .WithOne() // Assuming EventAction has no navigation back to Event
                   .HasForeignKey("EventId"); // Assuming foreign key in EventAction table

            builder.HasMany(e => e.EventMembers)
                   .WithOne() // Assuming EventMember has no navigation back to Event
                   .HasForeignKey("EventId");
        }
    }
}
