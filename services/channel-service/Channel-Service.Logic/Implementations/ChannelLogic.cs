using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChannelService.Data;
using ChannelService.Domain.DTOs;
using ChannelService.Domain.Entities;
using ChannelService.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ChannelService.Logic.Implementations
{
    public class ChannelLogic : IChannelLogic
    {
        private readonly ApplicationDbContext _context;

        public ChannelLogic(ApplicationDbContext context)
        {
            _context = context;
        }

        //Channel guild = _context.Channel.Include(e => e.Messages).FirstOrDefaultAsync(x => x.Id == guildId).Result;
        //    if (guild == null) return false;

        //    GuildUser user = guild.Users.Find(a => a.UserId == userId);
        //    if (user == null) return false;

        public Channel CreateChannel(CreateChannel channel)
        {

            EntityEntry<Channel> result = _context.Channel.Add(channel.ToEntity());
            _context.SaveChanges();

            return result.Entity;
        }

        public bool DeleteChannel(int ChannelId)
        {
            Channel channel = _context.Channel.Find(ChannelId);

            if (channel == null)
            {
                return false;
            }
            _context.Channel.Remove(channel);
            _context.SaveChanges();
            return true;
        }

        // TODO: protect this endpoint, internal usage only
        public Channel FindChannelById(int id)
        {
            Channel channel = _context.Channel.FirstOrDefaultAsync(x => x.Id == id).Result;

            return channel;
        }

        public bool UpdateChannel(int id, CreateChannel createchannel)
        {
            Channel channel = _context.Channel.Find(id);

            channel.Name = createchannel.Name;

            _context.SaveChanges();
            return true;
        }
    }
}
