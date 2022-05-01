using Demo.Reviews;
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
                    Id = 1,
                    Name = "Ada Lovelace",
                    Birthdate=new DateTime(1815, 12, 10),
                    UserName="@ada"
                },
                new User()
                {
                    Id=2,
                    Name ="Alan Turing",
                    Birthdate= new DateTime(1912, 06, 23),
                    UserName="@complete"
                }
            }.ToDictionary(t => t.Id);

        }

        public User GetUser(int id)
        {
            var user = _users[id];
            user.reviews = new ReviewRepository().GetReviewsByAuthorId(id).ToArray();
            return user;
        }

        public IEnumerable<User> GetUsers() => _users.Values;
    }
}