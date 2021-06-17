using System;
using System.Collections.Generic;
using System.Text;
using GuildService.Domain.Entities;

namespace GuildService.Domain.DTOs
{
    public class AddUser
    {

        public string Name { get; set; }

        public int UserId { get; set; }

        public GuildUser ToEntity()
        {
            return new GuildUser()
            {
                UserId = UserId,
                Nickname = Name,
            };
        }
    }
}
