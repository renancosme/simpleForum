using SimpleForum.Domain.Entities;
using SimpleForum.Domain.Repository;

namespace SimpleForum.Infrastructure.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(SimpleForumContext context): base(context)
        {
        }
    }
}
