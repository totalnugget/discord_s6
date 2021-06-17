using System;
using System.Collections.Generic;
using System.Text;
using ChannelService.Domain.DTOs;
using ChannelService.Domain.Entities;

namespace ChannelService.Logic.Interfaces
{
    public interface IChannelLogic
    {
        Channel CreateChannel(CreateChannel channel);

        bool UpdateChannel(int id, CreateChannel channel);

        bool DeleteChannel(int id);

        Channel FindChannelById(int id);

    }
}
