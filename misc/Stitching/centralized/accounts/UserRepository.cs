 
using System;
using System.Collections.Generic;
using System.Linq;

namespace Demo.Accounts
{
    public class UserRepository
    {
        private readonly Dictionary<int, User> _users;

        public UserRepository()
        { 
            _users = new User[]
            {
                new User()
                {
                    id = 1,
                    name = "Ada Lovelace",
                    birthdate=new DateTime(1815, 12, 10),
                    username="@ada"
                },
                new User()
                {
                    id=2,
                    name ="Alan Turing",
                    birthdate= new DateTime(1912, 06, 23),
                    username="@complete"
                }
            }.ToDictionary(t => t.id);

        }

        public User GetUser(int id)
        {
            var user = _users[id];
           // user.reviews = new ReviewRepository().GetReviewsByAuthorId(id).ToArray();
            return user;
        }

        public IEnumerable<User> GetUsers() => _users.Values;
    }
}