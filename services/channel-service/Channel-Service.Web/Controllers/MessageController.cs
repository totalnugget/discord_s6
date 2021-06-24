using System;
using System.Collections.Generic;
using System.Linq;
using ChannelService.Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ChannelService.Domain.Entities;
using ChannelService.Domain.DTOs;
using FitKidRabbitMQClient.Interfaces;
using ChannelService.Domain.messages;
using System.Net.Http;
using Microsoft.AspNetCore.WebUtilities;

namespace ChannelService.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MessageController : Controller
    {
        private readonly IChannelLogic _ChannelLogic;
        private readonly IMessageLogic _MessageLogic;
        private static readonly HttpClient client = new HttpClient();

        public MessageController(IChannelLogic channelLogic, IMessageLogic messageLogic) {
            _ChannelLogic = channelLogic;
            _MessageLogic = messageLogic;
        }

        [HttpPost("{channelId:int}")]
        public ActionResult<Message> CreateMessage(int channelId, CreateMessage newMessage)
        {

            string url = Environment.GetEnvironmentVariable("FAAS_BadWords");
            var query = new Dictionary<string, string>
            {
                ["text"] = newMessage.Content,
            };
            var response = client.GetAsync(QueryHelpers.AddQueryString(url, query)).Result;
            string filteredMessage = response.Content.ReadAsStringAsync().Result;
            newMessage.Content = filteredMessage;

            var result = _MessageLogic.CreateMessage(channelId, newMessage);
            if (result != null)
            {
                return StatusCode(201, result);
            }

            return StatusCode(400);
        }

        [HttpPatch("{channelId:int}")]
        public ActionResult<Channel> UpdateMessage(int channelId, CreateMessage newMessage)
        {

            var result = _MessageLogic.UpdateMessage(channelId, newMessage);

            if (!result)
            {
                return StatusCode(404);
            }

            return StatusCode(200, result);
        }

        [HttpDelete("{MessageId:int}")]
        public ActionResult DeleteMessage(int MessageId)
        {
            bool result = _MessageLogic.DeleteMessage(MessageId);

            if (result)
            {
                return StatusCode(200);
            }

            return StatusCode(404);
        }

        [HttpGet("{MessageId:int}")]
        public ActionResult<Message> FindMessageById(int MessageId)
        {

           var result = _MessageLogic.FindMessageById(MessageId);

            if(result == null)
            {
                return StatusCode(404);
            }

            return StatusCode(200, result);
        }

        [HttpGet("all/{channelId:int}")]
        public ActionResult<Message> GetMessages(int channelId, int amount, DateTime beforeTime)
        {
            var result = _MessageLogic.GetMessages(channelId, beforeTime, amount);

            if (result == null)
            {
                return StatusCode(404);
            }

            return StatusCode(200, result);
        }

    }
}