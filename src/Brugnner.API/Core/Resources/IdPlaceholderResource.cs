namespace Brugnner.API.Core.Resources
{
    /// <summary>
    /// Holds the identifier of a resource.
    /// </summary>
    /// <typeparam name="TKey">Type of the id.</typeparam>
    public class IdPlaceholderResource<TKey> : APIResource
    {
        /// <summary>
        /// Id of the resource.
        /// </summary>
        public TKey Id { get; set; }
    }
}
