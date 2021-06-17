using GuildService.Domain.Entities;
using GuildService.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuildService.Logic.Implementations
{
    public class ChannelLogic : IChannelLogic
    {
        public ChannelPos AddChannel(int guildId, ChannelPos channel)
        {
            throw new NotImplementedException();
        }

        public ChannelPos FindChannelById(int guildId, int userId)
        {
            throw new NotImplementedException();
        }

        public List<GuildUser> GetAllChannel(int guildId)
        {
            throw new NotImplementedException();
        }

        public bool RemoveChannel(int guildId, int channelId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateChannel(int guildId, ChannelPos channel)
        {
            throw new NotImplementedException();
        }
    }
}
