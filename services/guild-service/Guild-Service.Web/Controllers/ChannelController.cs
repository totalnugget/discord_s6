using System;
using System.Collections.Generic;
using System.Linq;
using GuildService.Domain.Enums;
using GuildService.Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using GuildService.Domain.Entities;
using GuildService.Domain.DTOs;
using FitKidRabbitMQClient.Interfaces;
using GuildService.Domain.messages;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace GuildService.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ChannelController : Controller
    {
        private readonly IGuildLogic _guildLogic;
        private readonly IChannelLogic _channelLogic;
        private readonly IMessagePublisher _messagePublisher;
        private static readonly HttpClient client = new HttpClient();

        public ChannelController(IGuildLogic GuildLogic, IChannelLogic channelLogic, IMessagePublisher messagePublisher) {
            _guildLogic = GuildLogic;
            _channelLogic = channelLogic;
            _messagePublisher = messagePublisher;
        }

        //bool deleteGuild(int id);
        [HttpGet("{guildId:int}")]
        public ActionResult<List<ChannelPos>> GetAllChannels(int guildId)
        {

            var result = _channelLogic.GetAllChannels(guildId);

            if (result == null)
            {
                return StatusCode(404);
            }

            return StatusCode(200, result);
        }

        [HttpGet("{guildId:int}/{ChannelId:int}")]
        public ActionResult<List<GuildUser>> FindChannel(int guildId, int ChannelId)
        {

            var result = _channelLogic.GetAllChannels(guildId);

            if (result == null)
            {
                return StatusCode(404);
            }

            return StatusCode(200, result);
        }

        [HttpPost("{guildId:int}")]
        public async Task<ActionResult> AddChannel (int guildId, ChannelPos channel)
        {
            HttpResponseMessage resultUser = null;
            if (channel.ChannelId != 0)
            {
                resultUser = await client.GetAsync("http://channel-service" + "/Channel/" + channel.ChannelId);
                Console.WriteLine("[http] received channel: " + resultUser.Content);
            }
            else
            {
                var content = new CreateChannel { Name = channel.Name };
                var stringContent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
                resultUser = await client.PostAsync("http://channel-service" + "/Channel", stringContent);
                Console.WriteLine("[http] created channel: " + resultUser.Content);

                string responseBody =  await resultUser.Content.ReadAsStringAsync();
                var receivedchannel = JsonConvert.DeserializeObject<Channel>(responseBody);
                channel.ChannelId = receivedchannel.Id;
            }

            
            if (!resultUser.IsSuccessStatusCode)
            {
                return StatusCode(400, $"Could not create channel");
            }

            var result = _channelLogic.AddChannel(guildId, channel);
            if (result != null)
            {
                return StatusCode(201, result);
            }

            return StatusCode(400);
        }

        [HttpDelete("{guildId:int}/{id:int}")]
        public ActionResult RemoveChannel(int guildId, int id)
        {
            client.DeleteAsync("http://channel-service" + "/Channel/" + id);


            bool isremoved = _channelLogic.RemoveChannel(guildId, id);

            if (isremoved)
            {
                return StatusCode(200);
            }

            return StatusCode(404);
        }

        [HttpPatch("{guildId:int}/{id:int}")]
        public ActionResult UpdateChannel(int guildId, ChannelPos channel)
        {
            bool isremoved = _channelLogic.UpdateChannel(guildId, channel);

            if (isremoved)
            {
                return StatusCode(200);
            }

            return StatusCode(404);
        }

        //bool updateGuild(Guild guild);



        //Guild FindGuildByGUID(Guid guid);
    }
}