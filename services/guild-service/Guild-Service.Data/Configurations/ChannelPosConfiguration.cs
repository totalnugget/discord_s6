using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using GuildService.Domain.Entities;
using GuildService.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace GuildService.Data.Configurations
{
    public class ChannelPosConfiguration : IEntityTypeConfiguration<ChannelPos>
    {
        public void Configure(EntityTypeBuilder<ChannelPos> builder)
        {
            //base.Configure(builder);
            
            builder.HasKey(a => a.ChannelId);
            builder.Property(a => a.ChannelId).ValueGeneratedNever().IsRequired();// same id as in channel service, don't generate

            builder.Property(a => a.Name).IsRequired();
            builder.Property(a => a.Position).IsRequired();

            // base values
            builder.HasData(new
            {
                ChannelId = 1,
                Name = "general",
                Position = 1,
                GuildId = 1
            }
            );
        }
    }
}

