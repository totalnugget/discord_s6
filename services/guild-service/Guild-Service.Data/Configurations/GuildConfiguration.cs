using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using GuildService.Domain.Entities;

namespace GuildService.Data.Configurations
{
    public class GuildConfiguration : BaseEntityConfiguration<Guild>
    {
        public override void Configure(EntityTypeBuilder<Guild> builder)
        {
            base.Configure(builder);

            builder.Property(s => s.Name).IsRequired();

            // base values
            var time = DateTime.UtcNow;
            builder.HasData(new Guild
            { 
                Id = 1,
                LastUpdatedAt = time,
                CreatedAt = time,
                Name = "club house",
                Description = "talk en play zone for friends",
                Region = "eu",            }
            );
        }
    }
}

