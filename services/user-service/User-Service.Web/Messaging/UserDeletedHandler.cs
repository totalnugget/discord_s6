using FitKidRabbitMQClient.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Domain.messages;
using UserService.Logic.Interfaces;

namespace UserService.Web.Messaging
{
    public class UserDeletedHandler : IMessageHandler<UserDeleted>
    {
        private readonly IUserLogic _logic;
        public UserDeletedHandler(IUserLogic Logic)
        {
            _logic = Logic;
        }

        public Task HandleMessageAsync(string messageType, UserDeleted message)
        {
            Console.WriteLine("[UserDeletedHandler]  received message");
            _logic.deleteUser(message.Id);

            return Task.CompletedTask;
        }
    }
}
