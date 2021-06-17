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

namespace GuildService.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GuildController : Controller
    {
        private readonly IGuildLogic _Logic;
        private readonly IMessagePublisher _messagePublisher;

        public GuildController(IGuildLogic Logic, IMessagePublisher messagePublisher) {
            _Logic = Logic;
            _messagePublisher = messagePublisher;
        }

        [HttpPost("")]
        public ActionResult createGuild(GuildCreate guild)
        {
            var result = _Logic.CreateGuild(guild);
            if (result != null)
            {
                return StatusCode(201, result);
            }

            return StatusCode(400);
        }

        [HttpDelete("{id:int}")]
        public ActionResult deleteGuild(int id)
        {
            Guild guild = _Logic.FindGuildById(id);

            if (_Logic.DeleteGuild(id))
            {
                return StatusCode(200);
            }

            return StatusCode(404);
        }


        //bool updateGuild(Guild guild);

        //bool deleteGuild(int id);
        [HttpGet("{id}")]
        public ActionResult<Guild> FindGuildById(int id)
        {

           var result = _Logic.FindGuildById(id);

            if(result == null)
            {
                return StatusCode(404);
            }

            return StatusCode(201, result);
        }

        [HttpGet("/name/{name}")]
        public ActionResult<Guild> FindGuildByName(string name)
        {

            var result = _Logic.FindGuildByName(name);

            if (result == null)
            {
                return StatusCode(404);
            }

            return StatusCode(201, result);
        }

        //Guild FindGuildByGUID(Guid guid);
    }
}