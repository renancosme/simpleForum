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

        public static Topic New(string description, string title, int userId)
        {
            return new Topic
            {
                Description = description,
                Date = DateTime.Now,
                Title = title,
                UserId = userId
            };
        }
    }
}
