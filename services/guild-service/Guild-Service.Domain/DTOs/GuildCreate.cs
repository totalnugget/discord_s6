using System;
using System.Collections.Generic;
using System.Text;
using GuildService.Domain.Entities;

namespace GuildService.Domain.DTOs
{
    public class GuildCreate
    {

        public string Name { get; set; }

        public string Description { get; set; }

        public int OwnerId { get; set; }

        public string OwnerName { get; set; }

        public Guild ToEntity()
        {
            return new Guild
            {
                Name = Name,
                Description = Description,
                Region = "eu",
                Users = new List<GuildUser>() { new GuildUser() { UserId = OwnerId, IsOwner = true, Nickname = OwnerName, Permissions = Enums.Permissions.all} }
            };
        }
    }
}
