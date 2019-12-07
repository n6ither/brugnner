using Brugnner.API.Core.Contracts.Repositories;
using Brugnner.API.Core.Contracts.Services;
using Brugnner.API.Core.Services;
using Brugnner.API.Infrastructure.Persistence.Repositories;
using Brugnner.API.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Brugnner.API.Core.Extensions
{
    /// <summary>
    /// A collection of <see cref="IServiceCollection"/> extension methods.
    /// </summary>
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Register the services needed by the application.
        /// </summary>
        /// <param name="services"></param>
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IDbInitializerService, DbInitializerService>();
            services.AddScoped<IPostService, PostService>();

            services.AddSingleton<IPostRepository, PostRepository>();
        }

        /// <summary>
        /// Adds the JWT authentication mechanism.
        /// </summary>
        /// <param name="services"></param>
        public static void AddJWTAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = "https://brugnner-dev.auth0.com/";
                options.Audience = "https://brugnner.dev";
            });
        }

        /// <summary>
        /// Adds the API documentation using Swagger.
        /// </summary>
        /// <param name="services"></param>
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info
                {
                    Title = "Brugnner API",
                    Version = "v1",
                    Description = "Welcome to the backyard!",
                    Contact = new Contact
                    {
                        Email = "nery.brugnoni@gmail.com",
                        Name = "Nery Brugnoni",
                        Url = "https://brugnner.azurewebsites.net/"
                    }
                });

                options.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    In = "header",
                    Description = "Bearer + JWT",
                    Name = "Authorization",
                    Type = "apiKey"
                });

                options.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> { { "Bearer", Enumerable.Empty<string>() } });

                var xmlPath = Path.Combine(AppContext.BaseDirectory, "Brugnner.API.xml");
                options.IncludeXmlComments(xmlPath);

                options.DescribeAllEnumsAsStrings();
            });
        }

        /// <summary>
        /// Register a CORS policy.
        /// </summary>
        /// <param name="services"></param>
        public static void AddCORS(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader().AllowCredentials();
                    builder.WithOrigins("https://localhost:4200").AllowAnyMethod().AllowAnyHeader().AllowCredentials();
                    builder.WithOrigins("https://localhost:44300").AllowAnyMethod().AllowAnyHeader().AllowCredentials();
                });
            });
        }
    }
}
