using SimpleForum.Domain.Entities;
using SimpleForum.Domain.ValueObjects;
using System.Collections.Generic;

namespace SimpleForum.Domain.Repository
{
    public interface ITopicRepository : IRepository<Topic>
    {
        List<TopicListItemDTO> GetTopicListItems(int pageIndex, int pageSize = 10);
        Topic GetTopicWithPosts(int id);
    }
}
