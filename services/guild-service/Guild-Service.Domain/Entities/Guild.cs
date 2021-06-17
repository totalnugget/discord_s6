using GuildService.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuildService.Domain.Entities
{
    public class Guild : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Region { get; set; }

        public List<ChannelPos> Channels { get; set; } = new List<ChannelPos>();

        public List<GuildUser> Users { get; set; } = new List<GuildUser>();
    }
}
