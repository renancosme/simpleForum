using System;

namespace SimpleForum.Domain.Entities
{
    public class Post
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public int TopicId { get; set; }

        public Topic Topic { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
