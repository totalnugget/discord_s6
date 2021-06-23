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
using System.Net.Http;
using System.Threading.Tasks;

namespace GuildService.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IGuildLogic _guildLogic;
        private readonly IUserLogic _userLogic;
        private readonly IMessagePublisher _messagePublisher;
        private static readonly HttpClient client = new HttpClient();

        public UserController(IGuildLogic GuildLogic, IUserLogic userLogic, IMessagePublisher messagePublisher) {
            _guildLogic = GuildLogic;
            _userLogic = userLogic;
            _messagePublisher = messagePublisher;
        }

        //bool deleteGuild(int id);
        [HttpGet("{guildId:int}/{id:int}")]
        public ActionResult<GuildUser> FindUserById(int guildId, int id)
        {

            var result = _userLogic.FindUserById(guildId, id);

            if (result == null)
            {
                return StatusCode(404);
            }

            return StatusCode(200, result);
        }

        [HttpGet("{guildId:int}")]
        public ActionResult<List<GuildUser>> FindUsersByGuildId(int guildId)
        {

            var result = _userLogic.GetAllUsers(guildId);

            if (result == null)
            {
                return StatusCode(404);
            }

            return StatusCode(200, result);
        }

        [HttpPost("{guildId:int}")]
        public async Task<ActionResult> addUser(int guildId, AddUser user)
        {
            var resultUser = await client.GetAsync("http://user-service" + "/user/" + guildId);

            Console.WriteLine(resultUser);

            var result = _userLogic.AddUser(guildId, user.UserId, user.Name);
            if (result != null)
            {
                return StatusCode(201, result);
            }

            return StatusCode(400);
        }

        [HttpDelete("{guildId:int}/{id:int}")]
        public ActionResult removeUser(int guildId, int id)
        {
            bool isremoved = _userLogic.RemoveUser(guildId, id);

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