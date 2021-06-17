﻿using System;
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
    public class MessageLogic : IMessageLogic
    {
        private readonly ApplicationDbContext _context;

        public MessageLogic(ApplicationDbContext context)
        {
            _context = context;
        }

        //Channel guild = _context.Channel.Include(e => e.Messages).FirstOrDefaultAsync(x => x.Id == guildId).Result;
        //    if (guild == null) return false;

        //    GuildUser user = guild.Users.Find(a => a.UserId == userId);
        //    if (user == null) return false;

        public Message CreateMessage(int channelId, CreateMessage message)
        {
            var channel = _context.Channel.Find(channelId);

            if (channel == null) return null;

            Message newmessage = message.ToEntity();

            channel.Messages.Add(newmessage);
            _context.SaveChanges();

            return newmessage;
        }

        public bool UpdateMessage(int id, CreateMessage newmessage)
        {
            Message message = _context.Message.Find(id);

            message.Content = newmessage?.Content;

            _context.SaveChanges();
            return true;
        }

        public bool DeleteMessage(int id)
        {
            Message message = _context.Message.Find(id);

            _context.Message.Remove(message);
            _context.SaveChanges();
            return true;
        }

        public Message FindMessageById(int id)
        {
            return _context.Message.Find(id);
        }

        public List<Message> FindMessagesByUserId(int UserId)
        {
            return _context.Message.Where(x => x.AuthorUserId == UserId).ToList();
        }
    }
}