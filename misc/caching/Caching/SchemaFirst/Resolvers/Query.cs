using Demo.Data;
using HotChocolate;
using HotChocolate.Types;


namespace Demo.Resolvers;

public class Query
{
    // Note : the paging attribute will rewrite the schema to a cursor paging structure.
    
    public IEnumerable<User> GetUsers([Service] PersonRepository repository) 
        => repository.GetUsers();
}
