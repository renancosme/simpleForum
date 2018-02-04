using Microsoft.EntityFrameworkCore;
using SimpleForum.Domain.Entities;
using SimpleForum.Domain.Repository;
using SimpleForum.Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;

namespace SimpleForum.Infrastructure.Repository
{
    public class TopicRepository : Repository<Topic>, ITopicRepository
    {
        public TopicRepository(SimpleForumContext context): base(context)
        {
        }
        
        public List<TopicListItemDTO> GetTopicListItems(int pageIndex, int pageSize)
        {
            var query = SimpleForumContext.Topics
                                    .Skip((pageIndex - 1) * pageSize)
                                    .Take(pageSize)                                    
                                    .OrderByDescending(t => t.Date)
                                    .Select(t => new {
                                        Id = t.Id,
                                        Title = t.Title,
                                        UserName = t.User.Name,
                                        Date = t.Date,
                                        TotalPosts = t.Posts.Count()
                                    });

            var topics = new List<TopicListItemDTO>();
            foreach (var item in query)
            {
                topics.Add(new TopicListItemDTO
                {
                    Id = item.Id,
                    Title = item.Title,
                    UserName = item.UserName,
                    Date = item.Date,
                    TotalPosts = item.TotalPosts
                });
            }

            return topics;
        }

        public Topic GetTopicWithPosts(int id)
        {
            return SimpleForumContext.Topics
                .Include(t => t.Posts)
                    .ThenInclude(p => p.User)
                .Include(t => t.User)
                //.Include(t => t.Posts.Select(p => p.User)) ---> Before .Net Core
                .Where(t => t.Id == id)
                .FirstOrDefault();
        }

        public SimpleForumContext SimpleForumContext
        {
            get
            {
                return _context as SimpleForumContext;
            }
        }
    }
}


