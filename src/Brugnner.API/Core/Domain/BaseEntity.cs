using System.Xml.Serialization;

namespace Brugnner.API.Core.Domain
{
    /// <summary>
    /// Represents an entity.
    /// </summary>
    /// <typeparam name="TKey">The type of the entity id.</typeparam>
    public abstract class BaseEntity<TKey>
    {
        /// <summary>
        /// Id.
        /// </summary>
        [XmlElement]
        public TKey Id { get; set; }
    }
}
