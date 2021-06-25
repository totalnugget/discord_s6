using ChannelService.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChannelService.Domain.Entities
{
    public class Message : BaseEntity
    {
        // foreign key
        public int AuthorUserId { get; set; }
         
        public string AuthorName { get; set; }

        public string Content { get; set; }

        public bool IsPinned { get; set; } = false;

        public int[] Mentions { get; set; }


    }
}
