using SimpleForum.Domain.Entities;
using SimpleForum.Domain.Repository;

namespace SimpleForum.Infrastructure.Repository
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(SimpleForumContext context): base(context)
        {
        }
    }
}
