using Brugnner.API.Core.Domain;
using System;
using System.Collections.Generic;

namespace Brugnner.API.Core.Contracts.Repositories
{
    /// <summary>
    /// Holds and manages a collection of <see cref="Post"/>.
    /// </summary>
    public interface IPostRepository : IRepository<Post, Guid>
    {
        /// <summary>
        /// Returns a collection of posts including those wich are not published.
        /// </summary>
        /// <returns></returns>
        ICollection<Post> GetAllAsAdmin();
    }
}
