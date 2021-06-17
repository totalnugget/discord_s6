using FitKidRabbitMQClient.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChannelService.Domain.messages;
using ChannelService.Logic.Interfaces;
using ChannelService.Domain.Entities;

namespace ChannelService.Web.Messaging
{
    public class UserDeletedHandler : IMessageHandler<UserDeleted>
    {
        private readonly IChannelLogic _channellogic;
        private readonly IMessageLogic _messageLogic;

        public UserDeletedHandler(IChannelLogic channellogic, IMessageLogic messageLogic)
        {
            _channellogic = channellogic;
            _messageLogic = messageLogic;
        }

        public Task HandleMessageAsync(string messageType, UserDeleted UserDeleted)
        {
            Console.WriteLine("[UserDeletedHandler]  received message");

            List<Message> messagesToDelete = _messageLogic.FindMessagesByUserId(UserDeleted.Id);
            //var channels = _channellogic.;

            foreach (Message message in messagesToDelete)
            {
                //TODO
                _messageLogic.DeleteMessage(message.Id);
            }

            return Task.CompletedTask;
        }
    }
}
