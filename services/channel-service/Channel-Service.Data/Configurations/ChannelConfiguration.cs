using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using ChannelService.Domain.Entities;

namespace ChannelService.Data.Configurations
{
    public class ChannelConfiguration : BaseEntityConfiguration<Channel>
    {
        public override void Configure(EntityTypeBuilder<Channel> builder)
        {
            base.Configure(builder);

            builder.Property(s => s.Name).IsRequired();

            // base values
            var time = DateTime.UtcNow;
            builder.HasData(new Channel
            { 
                Id = 1,
                Name = "general"         }
            );
        }
    }
}

