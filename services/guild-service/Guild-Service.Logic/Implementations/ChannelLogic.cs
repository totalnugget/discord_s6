using GuildService.Data;
using GuildService.Domain.Entities;
using GuildService.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuildService.Logic.Implementations
{
    public class ChannelLogic : IChannelLogic
    {
        private readonly ApplicationDbContext _context;

        public ChannelLogic(ApplicationDbContext context)
        {
            _context = context;
        }

        public ChannelPos AddChannel(int guildId, ChannelPos channel)
        {

            Guild guild = _context.Guild.Include(e => e.Channels).FirstOrDefaultAsync(v => v.Id == guildId).Result;
            if (guild == null) return null;


            guild.Channels.Add(channel);
            _context.SaveChanges();

            return channel;
        }

        public ChannelPos FindChannelById(int guildId, int channelId)
        {
            Guild guild = _context.Guild.Include(e => e.Channels).FirstOrDefaultAsync(v => v.Id == guildId).Result;
            if (guild == null) return null;

            ChannelPos channel = guild.Channels.Find(x => x.ChannelId == channelId);
            return channel;
        }

        public List<ChannelPos> GetAllChannels(int guildId)
        {
            Guild guild = _context.Guild.Include(e => e.Channels).FirstOrDefaultAsync(v => v.Id == guildId).Result;
            if (guild == null) return null;

            return guild.Channels;
        }

        public bool RemoveChannel(int guildId, int channelId)
        {
            Guild guild = _context.Guild.Include(e => e.Channels).FirstOrDefaultAsync(v => v.Id == guildId).Result;
            if (guild == null) return false;

            ChannelPos channel = guild.Channels.Find(x => x.ChannelId == channelId);

            guild.Channels.Remove(channel);
            _context.SaveChanges();
            return true;
        }

        public bool UpdateChannel(int guildId, ChannelPos channel)
        {
            Guild guild = _context.Guild.Include(e => e.Channels).FirstOrDefaultAsync(v => v.Id == guildId).Result;
            if (guild == null) return false;

            ChannelPos oldChannel = guild.Channels.Find(x => x.ChannelId == channel.ChannelId);

            oldChannel.Name = channel.Name;
            oldChannel.Position = channel.Position;

            _context.SaveChanges();
            return true;
        }
    }
}
