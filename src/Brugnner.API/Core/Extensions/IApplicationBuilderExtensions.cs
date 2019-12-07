using Brugnner.API.Core.Contracts.Services;
using Brugnner.API.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Brugnner.API.Core.Extensions
{
    /// <summary>
    /// A collection of <see cref="IApplicationBuilderExtensions"/> extension methods.
    /// </summary>
    public static class IApplicationBuilderExtensions
    {
        /// <summary>
        /// Register <see cref="ResponseWrapperMiddleware"/> in the application pipeline.
        /// </summary>
        /// <param name="app"></param>
        public static void UseResponseWrapperMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ResponseWrapperMiddleware>();
        }

        /// <summary>
        /// Initializes the database seeding process.
        /// </summary>
        /// <param name="app"></param>
        public static void UseDbInitializer(this IApplicationBuilder app)
        {
            var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var dbInitializer = scope.ServiceProvider.GetService<IDbInitializerService>();
                dbInitializer.SeedData();
            }
        }
    }
}
