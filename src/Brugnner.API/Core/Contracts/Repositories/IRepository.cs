using Brugnner.API.Core.Domain;
using System;
using System.Collections.Generic;

namespace Brugnner.API.Core.Contracts.Repositories
{
    /// <summary>
    /// Represents a generic repository.
    /// </summary>
    /// <typeparam name="TEntity">Entity type.</typeparam>
    /// <typeparam name="TKey">The entity id type.</typeparam>
    public interface IRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        /// <summary>
        /// Returns an entity.
        /// </summary>
        /// <param name="id">Entity's id.</param>
        /// <returns></returns>
        TEntity GetOne(TKey id);

        /// <summary>
        /// Returns an entity.
        /// </summary>
        /// <param name="predicate">Predicate to be applied in a single or default search.</param>
        /// <returns></returns>
        TEntity GetOne(Func<TEntity, bool> predicate);

        /// <summary>
        /// Returns all the entities.
        /// </summary>
        /// <returns></returns>
        ICollection<TEntity> GetAll();

        /// <summary>
        /// Save an entity to the file system.
        /// </summary>
        /// <param name="entity">Entity to save.</param>
        void Save(TEntity entity);

        /// <summary>
        /// Deletes an entity from the file system and cache.
        /// </summary>
        /// <param name="id">Entity's id.</param>
        void Delete(TKey id);

        /// <summary>
        /// Returns an enumerable with all the file paths related to the entity type.
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> GetEntitiesFilePaths();
    }
}
