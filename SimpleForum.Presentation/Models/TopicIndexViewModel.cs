using SimpleForum.Domain.ValueObjects;
using System.Collections.Generic;

namespace SimpleForum.Presentation.Models
{
    public class TopicIndexViewModel
    {
        public TopicIndexViewModel(List<TopicListItemDTO> topicListItemDTO)
        {
            Topics = topicListItemDTO;
        }
                
        public List<TopicListItemDTO> Topics { get; set; }        
    }
}
