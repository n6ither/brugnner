using Brugnner.API.Core.Contracts.Repositories;
using Brugnner.API.Core.Domain;
using Brugnner.API.Core.Exceptions;
using Brugnner.API.Core.Serializers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Brugnner.API.Infrastructure.Persistence.Repositories
{
    /// <summary>
    /// Represents a generic repository that works with XML files.
    /// </summary>
    /// <typeparam name="TEntity">Entity type.</typeparam>
    /// <typeparam name="TKey">The entity id type.</typeparam>
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly ILogger _logger;
        private ICollection<TEntity> Cache { get; set; }

        /// <summary>
        /// Path of the directory where the entities files are stored.
        /// </summary>
        public string FolderPath { get; set; }

        /// <summary>
        /// Creates a new instance of <see cref="Repository{TEntity, TKey}"/>.
        /// </summary>
        /// <param name="folderPath"> Path of the directory where the entities files are stored.</param>
        /// <param name="logger">Logger.</param>
        public Repository(string folderPath, ILogger logger)
        {
            FolderPath = folderPath;
            _logger = logger;

            RefreshCache();
        }

        /// <summary>
        /// Returns all the entities.
        /// </summary>
        /// <returns></returns>
        public virtual ICollection<TEntity> GetAll()
        {
            return Cache;
        }

        /// <summary>
        /// Returns an entity.
        /// </summary>
        /// <param name="id">Entity's id.</param>
        /// <returns></returns>
        public TEntity GetOne(TKey id)
        {
            return Cache.SingleOrDefault(x => x.Id.Equals(id));
        }

        /// <summary>
        /// Returns an entity.
        /// </summary>
        /// <param name="predicate">Predicate to be applied in a single or default search.</param>
        /// <returns></returns>
        public TEntity GetOne(Func<TEntity, bool> predicate)
        {
            return Cache.SingleOrDefault(predicate);
        }

        /// <summary>
        /// Save an entity to the file system.
        /// </summary>
        /// <param name="entity">Entity to save.</param>
        public void Save(TEntity entity)
        {
            string filePath = GetFilePath(entity);
            string xml = XmlSerializer.Serialize(entity);

            EnsureEntityFolderExists();

            using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
            using (var sw = new StreamWriter(fs))
            {
                sw.Write(xml);
            }

            RefreshCache();
        }

        /// <summary>
        /// Deletes an entity from the file system and cache.
        /// </summary>
        /// <param name="id">Entity's id.</param>
        public void Delete(TKey id)
        {
            ThrowIfFileDontExists(id);

            foreach (string path in GetEntitiesFilePaths())
                if (Path.GetFileNameWithoutExtension(path).Equals(id.ToString()))
                    File.Delete(path);

            RefreshCache();
        }

        /// <summary>
        /// Returns an enumerable with all the file paths related to the entity type.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetEntitiesFilePaths()
        {
            return Directory.EnumerateFiles(FolderPath, "*.xml", SearchOption.TopDirectoryOnly);
        }

        private string GetFilePath(TEntity entity)
        {
            return Path.Combine(FolderPath, entity.Id + ".xml");
        }

        private void RefreshCache()
        {
            Cache = new List<TEntity>();

            EnsureEntityFolderExists();

            foreach (string fileName in GetEntitiesFilePaths())
            {
                string xml = File.ReadAllText(fileName);
                TEntity entity = XmlSerializer.Deserialize<TEntity>(xml);
                Cache.Add(entity);
            }
        }

        private void EnsureEntityFolderExists()
        {
            if (!Directory.Exists(FolderPath))
                Directory.CreateDirectory(FolderPath);
        }

        private void ThrowIfFileDontExists(TKey id)
        {
            if (!File.Exists(Path.Combine(FolderPath, id.ToString() + ".xml")))
                throw new BusinessException($"No file found associated with Id {id}");
        }
    }
}
