using SimpleForum.Domain;
using SimpleForum.Domain.Repository;
using SimpleForum.Infrastructure.Repository;

namespace SimpleForum.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SimpleForumContext _context;

        public UnitOfWork(SimpleForumContext context)
        {
            _context = context;
            Topics = new TopicRepository(_context);
            Posts = new PostRepository(_context);
            Users = new UserRepository(_context);
        }

        public ITopicRepository Topics { get; private set; }

        public IPostRepository Posts { get; private set; }

        public IUserRepository Users { get; private set; }

        public void Dispose()
        {
            _context.Dispose();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}
