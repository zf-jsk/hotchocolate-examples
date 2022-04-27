using Demo.Accounts;
using HotChocolate.Types;

namespace Accounts
{
    public class UserType:ObjectType<User>
    {
        protected override void Configure(IObjectTypeDescriptor<User> descriptor)
        {
            descriptor.Field(t => t.Username)
                .Use(next => async context =>
                {
                    await next(context);

                    if (context.Result is string s)
                    {
                        context.Result = s.ToUpper();
                    }
                });
        }
    }
}
