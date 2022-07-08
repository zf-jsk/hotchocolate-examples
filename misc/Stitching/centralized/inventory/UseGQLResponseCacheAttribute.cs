
using HotChocolate.Types;
using HotChocolate.Types.Descriptors;
using System.Collections.Concurrent;
using System.Reflection;

namespace Demo.Inventory
{
    public  class UseGQLResponseCacheAttribute : ObjectFieldDescriptorAttribute
    {
        private static ConcurrentDictionary<string, dynamic> inMemCache = new ConcurrentDictionary<string, dynamic>();


        public UseGQLResponseCacheAttribute()
        {
        }

        public override void OnConfigure(IDescriptorContext context, IObjectFieldDescriptor descriptor, MemberInfo member)
        {
            var isResolverCahce = false;
            descriptor.Use(next => async context =>
            {
                var arguments = string.Empty;
                foreach(var argument in context.FieldSelection.Arguments)
                {
                    arguments+=argument.Name.Value +argument.Value;
                }
                if (context.FieldSelection.Directives != null)
                {
                    foreach(var directive in context.FieldSelection.Directives)
                    {
                        if(directive.Name.Value == "cacheControl")
                        {
                            isResolverCahce = true;
                        }
                    }
                }
                var queryPath = context.Path.Print()+ arguments;
                if (!inMemCache.ContainsKey(queryPath))
                {
                    System.Console.WriteLine("@@@Before");
                    await next(context);
                    inMemCache.TryAdd(queryPath, context.Result);
                }
                else
                {
                    inMemCache.TryGetValue(queryPath, out var value);
                    context.Result = value;
                }
            });
        }
    }
}
