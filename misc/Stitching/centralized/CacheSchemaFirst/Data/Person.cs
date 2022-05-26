using HotChocolate.Caching;
using HotChocolate.Types;

namespace Demo.Data;

public class Person 
{
    public int Id { get; set; }
    public string Name { get; set; }

    /*protected override void Configure(IObjectTypeDescriptor descriptor)
    {
        descriptor.Name("Person");
        descriptor.Directive(new CacheControlDirective { Scope = CacheControlScope.Public, InheritMaxAge = true });
    }*/
}
