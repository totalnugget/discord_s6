using ChannelService.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChannelService.Domain.Entities
{
    public class Channel : BaseEntity
    {
        public string Name { get; set; }

        public List<Message> Messages { get; set; } = new List<Message>();
}
}
