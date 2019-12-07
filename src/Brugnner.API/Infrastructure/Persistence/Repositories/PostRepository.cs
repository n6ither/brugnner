using Brugnner.API.Core.Contracts.Repositories;
using Brugnner.API.Core.Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Brugnner.API.Infrastructure.Persistence.Repositories
{
    /// <summary>
    /// Holds and manages a collection of <see cref="Post"/>.
    /// </summary>
    public class PostRepository : Repository<Post, Guid>, IPostRepository
    {
        /// <summary>
        /// Creates a new instance of a <see cref="PostRepository"/>.
        /// </summary>
        /// <param name="environment">Hosting environment.</param>
        /// <param name="logger">Logger.</param>
        public PostRepository(IHostingEnvironment environment, ILogger<PostRepository> logger) : base(Path.Combine(environment.WebRootPath, "Posts"), logger)
        {

        }

        /// <summary>
        /// Returns a collection of published posts only.
        /// </summary>
        /// <returns></returns>
        public override ICollection<Post> GetAll()
        {
            return base.GetAll().Where(x => x.IsPublished).ToList();
        }

        /// <summary>
        /// Returns a collection of posts including those wich are not published.
        /// </summary>
        /// <returns></returns>
        public ICollection<Post> GetAllAsAdmin()
        {
            return base.GetAll();
        }
    }
}
