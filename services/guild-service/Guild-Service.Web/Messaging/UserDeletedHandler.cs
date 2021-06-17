using FitKidRabbitMQClient.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuildService.Domain.messages;
using GuildService.Logic.Interfaces;

namespace GuildService.Web.Messaging
{
    public class UserDeletedHandler : IMessageHandler<UserDeleted>
    {
        private readonly IUserLogic _userlogic;
        private readonly IGuildLogic _guildlogic;

        public UserDeletedHandler(IUserLogic userlogic, IGuildLogic guildlogic)
        {
            _userlogic = userlogic;
            _guildlogic = guildlogic;
        }

        public Task HandleMessageAsync(string messageType, UserDeleted message)
        {
            Console.WriteLine("[UserDeletedHandler]  received message");

            var guilds = _guildlogic.FindGuildsContainingUser(message.Id);

            foreach (var guild in guilds)
            {
                _userlogic.RemoveUser(guild.Id, message.Id);
            }

            return Task.CompletedTask;
        }
    }
}
