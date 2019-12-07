using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Brugnner.API.Core.Resources
{
    /// <summary>
    /// Represents a list of <see cref="APIResource"/> as the response of an API endpoint.
    /// </summary>
    /// <typeparam name="TResource">Type of the <see cref="APIResource"/>.</typeparam>
    [DataContract]
    public class ListResultResource<TResource> where TResource : APIResource
    {
        /// <summary>
        /// Items count.
        /// </summary>
        [DataMember]
        public int Total { get; }

        /// <summary>
        /// List of <see cref="APIResource"/>.
        /// </summary>
        [DataMember]
        public IList<TResource> Items { get; set; }

        /// <summary>
        /// Creates a new instance of <see cref="ListResultResource{TResource}"/>.
        /// </summary>
        /// <param name="rows"></param>
        public ListResultResource(IList<TResource> rows)
        {
            Items = rows;
            Total = rows.Count;
        }
    }
}
