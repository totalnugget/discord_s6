using System;
using System.Collections.Generic;
using System.Linq;
using ChannelService.Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ChannelService.Domain.Entities;
using ChannelService.Domain.DTOs;
using FitKidRabbitMQClient.Interfaces;
using ChannelService.Domain.messages;

namespace ChannelService.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ChannelController : Controller
    {
        private readonly IChannelLogic _Logic;
        private readonly IMessagePublisher _messagePublisher;

        public ChannelController(IChannelLogic Logic, IMessagePublisher messagePublisher) {
            _Logic = Logic;
            _messagePublisher = messagePublisher;
        }

        [HttpPost("")]
        public ActionResult createChannel(CreateChannel channel)
        {
            var result = _Logic.CreateChannel(channel);
            if (result != null)
            {
                return StatusCode(201, result);
            }

            return StatusCode(400);
        }

        [HttpPatch("{id:int}")]
        public ActionResult<Channel> UpdateChannel(int id, CreateChannel channel)
        {

            var result = _Logic.UpdateChannel(id, channel);

            if (!result)
            {
                return StatusCode(404);
            }

            return StatusCode(201, result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult deleteChannel(int id)
        {
            Channel channel = _Logic.FindChannelById(id);

            if (_Logic.DeleteChannel(id))
            {
                return StatusCode(200);
            }

            return StatusCode(404);
        }


        //bool updateChannel(Channel channel);

        //bool deleteChannel(int id);
        [HttpGet("{id:int}")]
        public ActionResult<Channel> FindChannelById(int id)
        {

           var result = _Logic.FindChannelById(id);

            if(result == null)
            {
                return StatusCode(404);
            }

            return StatusCode(200, result);
        }

        

        //Channel FindChannelByGUID(Guid guid);
    }
}