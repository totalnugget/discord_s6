using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using GuildService.Domain.Entities;
using GuildService.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace GuildService.Data.Configurations
{
    public class GuildUserConfiguration : IEntityTypeConfiguration<GuildUser>
    {
        public void Configure(EntityTypeBuilder<GuildUser> builder)
        {
            //base.Configure(builder);

            builder.HasKey(a => new { a.GuildId, a.UserId });
            builder.Property(a => a.Nickname).IsRequired();
            builder.Property(a => a.GuildId).IsRequired();
            builder.Property(a => a.UserId).IsRequired();

            // base values
            var time = DateTime.UtcNow;
            builder.HasData(new GuildUser
            {
                GuildId = 1,
                UserId = 1,
                Nickname = "harry lotions :)",
                IsOwner = true,
                Permissions = Permissions.SendMessages & Permissions.ViewChannels
            }
            );
        }
    }
}

