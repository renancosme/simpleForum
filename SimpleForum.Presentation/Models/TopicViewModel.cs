using SimpleForum.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimpleForum.Presentation.Models
{
    public class TopicViewModel
    {        
        [Required]
        [MaxLength(100, ErrorMessage = "O valor máximo é 100")]
        [MinLength(2)]
        public String Title { get; set; }

        [Required]
        [MaxLength(400)]
        [MinLength(2)]
        public String Description { get; set; }

        public List<Post> Posts { get; set; }
    }
}
