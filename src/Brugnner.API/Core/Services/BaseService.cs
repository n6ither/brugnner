using Brugnner.API.Core.Contracts.Repositories;
using Brugnner.API.Core.Domain;
using Brugnner.API.Core.Resources;

namespace Brugnner.API.Core.Services
{
    /// <summary>
    /// Represents a generic business entity service.
    /// </summary>
    /// <typeparam name="TEntity">Entity.</typeparam>
    /// <typeparam name="TKey">The type of the entity id.</typeparam>
    /// <typeparam name="TResource">The <see cref="APIResource"/> to be mapped between the entity.</typeparam>
    public abstract class BaseService<TEntity, TKey, TResource> where TEntity : BaseEntity<TKey> where TResource : APIResource
    {
        /// <summary>
        /// Returns the entity repository.
        /// </summary>
        /// <returns></returns>
        public abstract IRepository<TEntity, TKey> Repository();

        /// <summary>
        /// Returns a list of the entity mapped to the resource.
        /// </summary>
        /// <param name="listParams">Search and pagination parameters.</param>
        /// <returns></returns>
        public abstract ListResultResource<TResource> List(ListParamsResource listParams);
    }
}