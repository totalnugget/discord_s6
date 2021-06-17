using GuildService.Data;
using GuildService.Domain.Entities;
using GuildService.Domain.Enums;
using GuildService.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuildService.Logic.Implementations
{
    public class UserLogic : IUserLogic
    {

        private readonly ApplicationDbContext _context;

        public UserLogic(ApplicationDbContext context)
        {
            _context = context;
        }

        public GuildUser AddUser(int guildid, int userid, string name)
        {
            Guild guild = _context.Guild.Include(e => e.Users).FirstOrDefaultAsync(v => v.Id == guildid).Result;

            if (guild == null) return null;

            var newuser = new GuildUser
            {
                UserId = userid,
                Nickname = name,

            };

            guild.Users.Add(newuser);
            _context.SaveChanges();

            return newuser;
        }

        public GuildUser FindUserById(int guildid, int userid)
        {
            Guild guild = _context.Guild.Include(e => e.Users).FirstOrDefaultAsync(x => x.Id == guildid).Result;
            if (guild == null) return null;

            return guild.Users.Find(a => a.UserId == userid);
        }

        public List<GuildUser> GetAllUsers(int guildid)
        {
            Guild guild = _context.Guild.Include(e => e.Users).FirstOrDefaultAsync(x => x.Id == guildid).Result;
            if (guild == null) return null;

            return guild.Users;
        }

        public bool RemoveUser(int guildId, int userId)
        {
            Guild guild = _context.Guild.Include(e => e.Users).FirstOrDefaultAsync(x => x.Id == guildId).Result;
            if (guild == null) return false;

            int removedcount = guild.Users.RemoveAll(a => a.UserId == userId);
            _context.SaveChanges();

            return removedcount > 0;
        }

        public bool SetUserNickName(int guildId, int userId, string name)
        {
            Guild guild = _context.Guild.Include(e => e.Users).FirstOrDefaultAsync(x => x.Id == guildId).Result;
            if (guild == null) return false;

            GuildUser user = guild.Users.Find(a => a.UserId == userId);
            if (user == null) return false;

            user.Nickname = name;

            _context.SaveChanges();
            return true;
        }

        public bool SetUserPermissions(int guildId, int userId, Permissions perms)
        {
            Guild guild = _context.Guild.Include(e => e.Users).FirstOrDefaultAsync(x => x.Id == guildId).Result;
            if (guild == null) return false;

            GuildUser user = guild.Users.Find(a => a.UserId == userId);
            if (user == null) return false;

            user.Permissions = perms;

            _context.SaveChanges();
            return true;
        }
    }
}
