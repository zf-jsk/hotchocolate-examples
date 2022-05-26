using HotChocolate.Caching;
using HotChocolate.Types;

namespace Demo.Data;

public class User 
{
    public int id { get; set; }
    public string name { get; set; }
    public DateTime birthdate{get;set;} 
    public String Username{get;set; }

    /*protected override void Configure(IObjectTypeDescriptor descriptor)
    {
        descriptor.Name("Person");
        descriptor.Directive(new CacheControlDirective { Scope = CacheControlScope.Public, InheritMaxAge = true });
    }*/
}
