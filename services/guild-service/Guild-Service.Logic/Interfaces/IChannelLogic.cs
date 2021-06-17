
using GuildService.Domain.Entities;
using GuildService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuildService.Logic.Interfaces
{
    public interface IChannelLogic
    {
        ChannelPos FindChannelById(int guildId, int userId);

        List<GuildUser> GetAllChannel(int guildId);

        ChannelPos AddChannel(int guildId, ChannelPos channel);

        bool RemoveChannel(int guildId, int channelId);

        bool UpdateChannel(int guildId, ChannelPos channel);
    }
}
