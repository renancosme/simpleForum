using System;
using System.Collections.Generic;

namespace SimpleForum.Domain.Entities
{
    public class Topic
    {
        public int Id { get; set; }

        public String Title { get; set; }

        public String Description { get; set; }

        public DateTime Date { get; set; }

        public List<Post> Posts { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
