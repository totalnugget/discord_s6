
using GuildService.Domain.Entities;
using GuildService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuildService.Logic.Interfaces
{
    public interface IUserLogic
    {
        GuildUser FindUserById(int guildid, int userid);

        List<GuildUser> GetAllUsers(int guildid);

        GuildUser AddUser(int guildid, int userid, string name);

        bool RemoveUser(int guildId, int userId);

        bool SetUserNickName(int guildId, int userId, string name);

        bool SetUserPermissions(int guildId, int userId, Permissions perms);
    }
}
