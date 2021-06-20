using System;
using System.Collections.Generic;
using System.Text;
using ChannelService.Domain.Entities;

namespace ChannelService.Domain.DTOs
{
    public class CreateChannel
    {

        public string Name { get; set; }

        public Channel ToEntity()
        {
            return new Channel()
            {
                Name = Name
            };
        }
    }
}
