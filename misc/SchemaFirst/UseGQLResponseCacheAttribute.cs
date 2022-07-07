using HotChocolate.Types;
using HotChocolate.Types.Descriptors;
using System.Reflection;

namespace SchemaFirst
{

    public sealed class UseGQLResponseCacheAttribute : DescriptorAttribute
    {
        public UseGQLResponseCacheAttribute()
        { 
        }

        protected override void TryConfigure(IDescriptorContext context, IDescriptor descriptor, ICustomAttributeProvider element)
        {
            if (element is MemberInfo)
            {
            }
        }
    }
}
