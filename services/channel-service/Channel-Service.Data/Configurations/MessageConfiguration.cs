using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using ChannelService.Domain.Entities;

namespace ChannelService.Data.Configurations
{
    public class MessageConfiguration : BaseEntityConfiguration<Message>
    {
        public override void Configure(EntityTypeBuilder<Message> builder)
        {
            base.Configure(builder);

            builder.Property(s => s.AuthorUserId).IsRequired();
            builder.Property(s => s.AuthorName).IsRequired();
            builder.Property(s => s.IsPinned).IsRequired();
            builder.Property(s => s.Content).IsRequired();

            // base values
            var time = DateTime.UtcNow;
            builder.HasData(new
            {
                ChannelId = 1,

                Id = 1,
                AuthorName = "Harry",
                IsPinned = false,
                Content = "This is a default message",
                AuthorUserId = 1,
                CreatedAt = time,
                LastUpdatedAt = time
            });
            
        }
    }
}

