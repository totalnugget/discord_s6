using System;
using System.Collections.Generic;
using System.Text;
using ChannelService.Domain.DTOs;
using ChannelService.Domain.Entities;

namespace ChannelService.Logic.Interfaces
{
    public interface IMessageLogic
    {
        Message CreateMessage(int channelId, CreateMessage message);

        bool UpdateMessage(int id, CreateMessage message);

        bool DeleteMessage(int id);

        Message FindMessageById(int id);

        List<Message> FindMessagesByUserId(int UserId);

    }
}
