using System;
using System.Collections.Generic;
using System.Net.Http;
using HotChocolate;
using Microsoft.AspNetCore.Http;

namespace Demo.Accounts
{
    public class Query
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public  Query(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public IEnumerable<User> GetUsers( [Service] UserRepository repository)
        {
            return repository.GetUsers();
        }
            

        public User GetUser(int id, [Service] UserRepository repository) => 
            repository.GetUser(id);
    }

    public class SubQuery
    {
        public SubGraph Get_service() => new SubGraph();
    }

    public class SubGraph
    {

        public String sdl { get; set; } = System.IO.File.ReadAllText("./accounts.graphql");
    }
}