using Demo.Data;
using HotChocolate; 
using System.Collections.Generic;

namespace Demo.Resolvers
{

    public class Query
    {
        // Note : the paging attribute will rewrite the schema to a cursor paging structure.
        //[UsePaging]
        //public IEnumerable<Person> GetPersons([Service] PersonRepository repository)
        public IEnumerable<Person> GetPersons(PersonRepository repository)
        => repository.GetPersons();
    }
}