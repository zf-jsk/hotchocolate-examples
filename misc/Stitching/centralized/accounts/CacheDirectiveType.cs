using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Accounts
{
   // Input Type
    public class CacheInput
    {
        public string expiry { get; set; }
    }
    public class CacheDirectiveType : DirectiveType<CacheInput>
    {
        public static IDictionary<String, Object> CacheDictionary = new Dictionary<string, object>();

        protected override void Configure(IDirectiveTypeDescriptor<CacheInput> descriptor)
        {
            descriptor.Name("cache");
            descriptor.Location(DirectiveLocation.Schema|DirectiveLocation.FieldDefinition|DirectiveLocation.Field);
            descriptor
                    .Argument("expiry")
                    .Type<NonNullType<StringType>>();
            descriptor.Use(next => context =>
            {
                String dictionaryKey = context.Path.ToString() + context.Document.ToString();
                var vt = new ValueTask();
                if (CacheDictionary.ContainsKey(dictionaryKey))
                {
                    context.Result = CacheDictionary[dictionaryKey];
                }
                else
                {
                    vt = next.Invoke(context); 
                    CacheDictionary.Add(dictionaryKey, context.Result);
                } 
                return vt;
            });
            descriptor.BindArgumentsImplicitly().BindArgumentsExplicitly();
        }
    }
}
