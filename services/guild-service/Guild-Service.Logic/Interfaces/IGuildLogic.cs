using System;
using System.Collections.Generic;
using System.Text;
using GuildService.Domain.DTOs;
using GuildService.Domain.Entities;

namespace GuildService.Logic.Interfaces
{
    public interface IGuildLogic
    {
        Guild CreateGuild(GuildCreate guild);

        bool UpdateGuild(GuildCreate guild);

        bool DeleteGuild(int id);

        Guild FindGuildById(int id);

        Guild FindGuildByName(string name);

        List<Guild> FindGuildsContainingUser(int userId);

    }
}
