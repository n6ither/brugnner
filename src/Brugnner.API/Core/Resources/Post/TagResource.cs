using System.ComponentModel.DataAnnotations;

namespace Brugnner.API.Core.Resources.Post
{
    /// <summary>
    /// A tag associated with a blog post.
    /// </summary>
    public class TagResource
    {
        /// <summary>
        /// Name of the tag.
        /// </summary>
        [Required]
        public string Name { get; set; }
    }
}