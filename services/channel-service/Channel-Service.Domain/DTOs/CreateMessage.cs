using System;
using System.Collections.Generic;
using System.Text;
using ChannelService.Domain.Entities;

namespace ChannelService.Domain.DTOs
{
    public class CreateMessage
    {

        public string AuthorName { get; set; }

        public int AuthorUserId { get; set; }

        public string Content { get; set; }

        public List<int> Mentions { get; set; } = new List<int>();

        public Message ToEntity() => new Message()
        {
            AuthorName = AuthorName,
            AuthorUserId = AuthorUserId,
            Content = Content,
            Mentions = Mentions.ToArray(),
        };
    }
}
