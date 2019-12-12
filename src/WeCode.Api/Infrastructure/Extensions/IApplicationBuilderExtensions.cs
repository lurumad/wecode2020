using System;

namespace Microsoft.AspNetCore.Builder
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder AddIf(this IApplicationBuilder app, bool condition, Func<IApplicationBuilder, IApplicationBuilder> @if)
        {
            return condition ? @if(app) : app;
        }
    }
}
