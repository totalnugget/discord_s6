using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GuildService.Data;
using GuildService.Domain.DTOs;
using GuildService.Domain.Entities;
using GuildService.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GuildService.Logic.Implementations
{
    public class GuildLogic : IGuildLogic
    {
        private readonly ApplicationDbContext _context;

        public GuildLogic(ApplicationDbContext context)
        {
            _context = context;
        }


        public Guild CreateGuild(GuildCreate guild)
        {
            EntityEntry<Guild> result = _context.Guild.Add(guild.ToEntity());
            _context.SaveChanges();

            return result.Entity;
        }

        public bool DeleteGuild(int id)
        {
            Guild guild = _context.Guild.Find(id);

            if(guild == null)
            {
                return false;
            }
            _context.Guild.Remove(guild);
            _context.SaveChanges();
            return true;
        }

        // TODO: protect this endpoint, internal usage only
        public Guild FindGuildById(int id)
        {
            Guild guild = _context.Guild.Include(e => e.Users).FirstOrDefaultAsync(x => x.Id == id).Result;

            return guild;
        }

        public Guild FindGuildByName(string name)
        {
            Guild guild = _context.Guild.Where(a => a.Name == name).FirstOrDefault();

            return guild;
        }

        // protected call
        public List<Guild> FindGuildsContainingUser(int userId)
        {
            return _context.Guild.Include(e => e.Users).Where(g => g.Users.Any(u => u.UserId == userId)).ToList();

        }

        public bool UpdateGuild(GuildCreate guild)
        {
            throw new NotImplementedException();
        }
    }
}
