 
using HotChocolate.Resolvers;
using System.Threading.Tasks;

namespace Accounts
{
    public class FieldLevelMiddleware 
    {
        private readonly FieldDelegate _next;
       // private readonly IMySingletonService _singletonService;

        public FieldLevelMiddleware(FieldDelegate next)//, IMySingletonService singletonService)
        {
            _next = next;
           // _singletonService = singletonService;
        }

        public async Task InvokeAsync(IMiddlewareContext context)//, IMyScopedService scopedService)
        {
            // the middleware logic
            await _next(context);
        }
    }
}
