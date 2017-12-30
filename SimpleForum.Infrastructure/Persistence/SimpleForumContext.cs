using Microsoft.EntityFrameworkCore;
using SimpleForum.Domain.Entities;

namespace SimpleForum.Infrastructure
{
    public class SimpleForumContext : DbContext
    {
        public SimpleForumContext(DbContextOptions<SimpleForumContext> options) 
            : base(options)
        {
        }

        public DbSet<Topic> Topics { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Topic>(entity => {
                entity.Property(t => t.Id).IsRequired();
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Date);
                entity.Property(p => p.Title).HasMaxLength(100);
                entity.Property(p => p.Description).HasMaxLength(400);
                entity.HasMany(t => t.Posts)
                    .WithOne(p => p.Topic)
                    .HasForeignKey(p => p.TopicId);
            });

            modelBuilder.Entity<Post>(entity => {
                entity.Property(t => t.Id).IsRequired();
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Date).IsRequired();
                entity.Property(t => t.Description).IsRequired().HasMaxLength(400);
                entity.HasOne(t => t.Topic);
                entity.HasOne(t => t.User);
            });

            modelBuilder.Entity<User>(entity => {
                entity.Property(u => u.Id).IsRequired();
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Name).IsRequired().HasMaxLength(50);
                entity.Property(u => u.Password).IsRequired().HasMaxLength(20);
                entity.HasMany(e => e.Posts)
                   .WithOne(e => e.User)
                   .HasForeignKey(e => e.UserId);
                entity.HasMany(e => e.Topics)
                    .WithOne(e => e.User)
                    .HasForeignKey(e => e.UserId);
            });

            base.OnModelCreating(modelBuilder);
        }

    }
}
