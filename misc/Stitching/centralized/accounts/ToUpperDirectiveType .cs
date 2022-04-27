using HotChocolate.Resolvers;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Accounts
{
    //Input Type
    public class MyDirective
    {
        public string name { get; set; }
    }
    public class ToUpperDirectiveType : DirectiveType<MyDirective>
    {
        public static IDictionary<String, Object> CacheDictionary = new Dictionary<string, object>();
        protected override void Configure(IDirectiveTypeDescriptor<MyDirective> descriptor)
        {
            descriptor.Name("toUpper");
            descriptor.Location(DirectiveLocation.Field);
            descriptor
                    .Argument("name")
                    .Type<NonNullType<StringType>>();
            descriptor.Use(next => context =>
            {
                String dictionaryKey= context.Path.ToString()+context.Document.ToString();
                var vt=new ValueTask();
                if (CacheDictionary.ContainsKey(dictionaryKey))
                {
                    context.Result = CacheDictionary[dictionaryKey];
                }
                else
                {                   
                    vt = next.Invoke(context);
                    if (context.Directive.Name == "toUpper")
                    {
                        context.Result = context.Result.ToString().ToUpperInvariant();
                    }
                    CacheDictionary.Add(dictionaryKey, context.Result);
                }
                //static dictiona
                //Key QueryDoc+Path
                //Value Result
                //check before invoke if path found  take value from result and bind it to vt else resolver call
               //Post Response
                return vt;
            });
            //  descriptor.Use(_ => _ => default);
            descriptor.BindArgumentsImplicitly().BindArgumentsExplicitly();

        }
    }

    public class ToUpperDirectiveTypeMiddleware
    {
        private readonly FieldDelegate _next;

        public ToUpperDirectiveTypeMiddleware(FieldDelegate next)
        {
            _next = next;
        }

        public Task InvokeAsync(IDirectiveContext context)
        {
            return Task.CompletedTask;
        }
    }
}
