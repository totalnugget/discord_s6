using System;
using System.Collections.Generic;
using System.Linq;
using UserService.Domain.Enums;
using UserService.Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using UserService.Domain.Entities;
using UserService.Domain.DTOs;
using FitKidRabbitMQClient.Interfaces;
using UserService.Domain.messages;

namespace UserService.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserLogic _Logic;
        private readonly IMessagePublisher _messagePublisher;

        public UserController(IUserLogic Logic, IMessagePublisher messagePublisher) {
            _Logic = Logic;
            _messagePublisher = messagePublisher;
        }

        [HttpPost("")]
        public ActionResult createUser(UserCreate user)
        {
            var result = _Logic.createUser(user.ToEntity());
            if (result != null)
            {
                return StatusCode(201, result);
            }

            return StatusCode(400);
        }

        [HttpDelete("{id:int}")]
        public ActionResult deleteUser(int id)
        {
            User user = _Logic.FindUserById(id);

            //if (_Logic.deleteUser(id))
            //{
            //    return StatusCode(200);
            //}

            _messagePublisher.PublishMessageAsync<UserDeleted>("UserDeleted", new UserDeleted { Id = id, Name = user.Name });

            return StatusCode(400);
        }


        //bool updateUser(User user);

        //bool deleteUser(int id);
        [HttpGet("{id}")]
        public ActionResult<User> FindUserById(int id)
        {

           var result = _Logic.FindUserById(id);

            if(result == null)
            {
                return StatusCode(400);
            }

            return StatusCode(201, result);
        }

        [HttpGet("/name/{name}")]
        public ActionResult<User> FindUserByName(string name)
        {

            var result = _Logic.FindUserByName(name);

            if (result == null)
            {
                return StatusCode(400);
            }

            return StatusCode(201, result);
        }

        //User FindUserByGUID(Guid guid);
    }
}