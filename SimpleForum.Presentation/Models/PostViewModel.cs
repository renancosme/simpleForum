using SimpleForum.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleForum.Presentation.Models
{
    public class PostViewModel
    {
        public PostViewModel() { }

        public PostViewModel(Topic topicWithPostListDTO)
        {
            TopicAndPosts = topicWithPostListDTO;
            TopicId = topicWithPostListDTO.Id;
        }

        [Required]
        [MaxLength(400)]
        [MinLength(2)]
        public String Description { get; set; }

        public Topic TopicAndPosts { get; set; }

        public int TopicId { get; set; }
    }
}
