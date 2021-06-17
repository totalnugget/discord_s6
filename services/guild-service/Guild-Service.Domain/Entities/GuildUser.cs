using GuildService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuildService.Domain.Entities
{
    public class GuildUser
    {
        public int GuildId { get; set; }

        public int UserId { get; set; }

        public string Nickname { get; set; }

        public Permissions Permissions { get; set; } = Permissions.SendMessages | Permissions.ViewChannels;

        public bool IsOwner { get; set; } = false;
    }
}
