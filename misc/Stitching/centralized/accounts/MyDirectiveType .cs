using HotChocolate.Types;

namespace Accounts
{
    //Input Type
    public class MyDirective
    {
        public string Name { get; set; }
    }
    public class MyDirectiveType: DirectiveType<MyDirective>
{
    protected override void Configure(IDirectiveTypeDescriptor<MyDirective> descriptor)
    {
        descriptor.Name("my");
        descriptor.Location(DirectiveLocation.Field);
        descriptor
                .Argument("name")
                .Type<NonNullType<StringType>>();
        descriptor.Use(next => context =>
        {
            //context.Result = 1;
            return next.Invoke(context);
        });

        }
}
}
