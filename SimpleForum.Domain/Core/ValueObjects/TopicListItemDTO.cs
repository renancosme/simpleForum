using System;

namespace SimpleForum.Domain.ValueObjects
{
    public class TopicListItemDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string UserName { get; set; }

        public DateTime Date { get; set; }

        public int TotalPosts { get; set; }
    }
}
