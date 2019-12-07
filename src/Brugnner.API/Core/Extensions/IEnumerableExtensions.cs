using AutoMapper;
using Brugnner.API.Core.Domain;
using Brugnner.API.Core.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Brugnner.API.Core.Extensions
{
    /// <summary>
    /// A collection of <see cref="IEnumerable{T}"/> extension methods.
    /// </summary>
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Takes an <see cref="IEnumerable{T}"/> of <see cref="BaseEntity{TKey}"/> and returns a <see cref="ListResultResource{TResource}"/> applying search, filter, order and pagination parameters.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TKey">Type of the id of the entity.</typeparam>
        /// <typeparam name="TResource">Type of the <see cref="APIResource"/>.</typeparam>
        /// <param name="entities">Enumerable of entities.</param>
        /// <param name="listParams">Search and paginations parameters.</param>
        /// <param name="mapper">Mapper.</param>
        /// <returns></returns>
        public static ListResultResource<TResource> MapToListResultResource<TEntity, TKey, TResource>(this IEnumerable<TEntity> entities, ListParamsResource listParams, IMapper mapper)
            where TEntity : BaseEntity<TKey>, new()
            where TResource : APIResource, new()
        {
            var rows = entities
                .ApplyFilters(listParams.Filters)
                .ApplyMapping<TEntity, TResource>(mapper)
                .ApplyPagination(listParams)
                .ToList();

            return new ListResultResource<TResource>(rows);
        }

        private static IEnumerable<TEntity> ApplyFilters<TEntity>(this IEnumerable<TEntity> entities, string filter)
        {
            if (string.IsNullOrEmpty(filter))
                return entities;

            return entities.AsQueryable().Where(filter);
        }

        private static IEnumerable<TResource> ApplyMapping<TEntity, TResource>(this IEnumerable<TEntity> entities, IMapper mapper)
        {
            return mapper.Map<IEnumerable<TResource>>(entities);
        }

        private static IEnumerable<TResource> ApplyPagination<TResource>(this IEnumerable<TResource> resources, ListParamsResource listParams)
        {
            return resources
                .ApplyOrder(listParams.OrderByField, listParams.OrderByDirection)
                .ApplyLimits(listParams.Skip, listParams.Take);
        }

        private static IEnumerable<TResource> ApplyOrder<TResource>(this IEnumerable<TResource> resources, string orderByField, string orderByDirection)
        {
            if (!string.IsNullOrEmpty(orderByField))
            {
                var propertyInfo = typeof(TResource).GetProperties().FirstOrDefault(x => x.Name.ToLower() == orderByField.ToLower());

                if (propertyInfo == null)
                    throw new Exception($"Property '{orderByField}' is not part of '{typeof(TResource).Name}'");

                orderByDirection = orderByDirection.ToLower();

                if (!orderByDirection.Equals("asc") && !orderByDirection.Equals("desc"))
                    throw new Exception($"'{orderByDirection}' is not a valid order direction");

                resources = orderByDirection.Equals("desc") ? resources.OrderByDescending(x => propertyInfo.GetValue(x, null)) : resources.OrderBy(x => propertyInfo.GetValue(x, null));
            }

            return resources;
        }

        private static IEnumerable<TResource> ApplyLimits<TResource>(this IEnumerable<TResource> source, int skip, int take)
        {
            if (skip == 0 && take == 0)
                return source;

            return source.Skip(skip).Take(take);
        }
    }
}
