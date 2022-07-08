using HotChocolate;
using HotChocolate.Execution;
using HotChocolate.Language;
using HotChocolate.Types;
using HotChocolate.Types.Descriptors;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;

namespace Demo.Reviews
{
    public  class UseGQLResponseCacheAttribute : ObjectFieldDescriptorAttribute
    {
        private static ConcurrentDictionary<string, dynamic> inMemCache = new ConcurrentDictionary<string, dynamic>();

        private IList<String> reservedDirectives = new List<string>() { "skip","",""};
        public UseGQLResponseCacheAttribute()
        {
        }

        public override void OnConfigure(IDescriptorContext context, IObjectFieldDescriptor descriptor, MemberInfo member)
        {
            var isResolverCahce = false;
            descriptor.Use(next => async context =>
            {
                var arguments = string.Empty;
                foreach (var argument in context.FieldSelection.Arguments)
                    arguments += GetArgumentValue(argument, context.Variables);
               
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
        private String GetArgumentValue(ArgumentNode argumentNode,IVariableValueCollection variableValueCollection)
        {
            var argument = String.Empty;
            if (argumentNode.Value.ToString().Contains("$"))
                argument += GetVariableTypeArgument(argumentNode, variableValueCollection);
            else
                argument += GetValueTypeArgument(argumentNode);
            return argument;
        }

        private String GetValueTypeArgument(ArgumentNode argumentNode)
        {
            return argumentNode.Name.Value + argumentNode.Value; ;
        }
        private String GetVariableTypeArgument(ArgumentNode argumentNode,IVariableValueCollection variableValueCollection)
        {
            var arguments = String.Empty;
            NameString variableName = new NameString(argumentNode.Name.ToString());
            var variable = variableValueCollection.GetVariable<Object>(variableName);
            if (variable.GetType().Name.Contains("List"))//Collection
            {
                foreach(var variableIndex in variable as IEnumerable)
                {
                    arguments += variableIndex.ToString();
                }
            }
            else
            {
                arguments += variable.ToString();
            }
            return arguments;
        }
    }
}
