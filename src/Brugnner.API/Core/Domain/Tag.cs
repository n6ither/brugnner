using System.Xml.Serialization;

namespace Brugnner.API.Core.Domain
{
    /// <summary>
    /// A tag associated with a blog post.
    /// </summary>
    public class Tag
    {
        /// <summary>
        /// Name of the tag.
        /// </summary>
        [XmlText()]
        public string Name { get; set; }

        /// <summary>
        /// Creates a new instance of a tag.
        /// </summary>
        public Tag()
        {

        }

        /// <summary>
        /// Creates a new instance of a tag.
        /// </summary>
        /// <param name="name">Name of the tag.</param>
        public Tag(string name)
        {
            Name = name;
        }
    }
}
