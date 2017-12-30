using SimpleForum.Domain.Repository;
using System;

namespace SimpleForum.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        ITopicRepository Topics { get; }
        IPostRepository Posts { get; }
        IUserRepository Users { get; }
        int Save();
    }
}
