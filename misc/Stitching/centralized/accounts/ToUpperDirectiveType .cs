using HotChocolate.Resolvers;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Accounts
{
    //Input Type
    //public class MyDirective
    //{
    //    public string name { get; set; }
    //}
    public class ToUpperDirectiveType : DirectiveType 
    {
        
        protected override void Configure(IDirectiveTypeDescriptor  descriptor)
        {
            descriptor.Name("toUpper");
            descriptor.Location(DirectiveLocation.Field); 
            descriptor.Use(next => context =>
            {
                String dictionaryKey= context.Path.ToString()+context.Document.ToString();
                var vt= next.Invoke(context);
                if (context.Directive.Name == "toUpper")
                {
                    context.Result = context.Result?.ToString().ToUpperInvariant();
                }                 
                return vt;
            }); 
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
