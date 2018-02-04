using System.Collections.Generic;

namespace SimpleForum.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public List<Post> Posts { get; set; }

        public List<Topic> Topics { get; set; }

        public static User New(string name, string password)
        {
            return new User {
                Name = name,
                Password = password
            };
        }
    }
}
