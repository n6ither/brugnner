using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Brugnner.API.Core.Domain
{
    /// <summary>
    /// A blog post.
    /// </summary>
    [XmlRoot]
    public class Post : BaseEntity<Guid>
    {
        /// <summary>
        /// Title of the blog.
        /// </summary>
        [XmlElement]
        public string Title { get; set; }

        /// <summary>
        /// A little description of the post.
        /// </summary>
        [XmlElement]
        public string Excerpt { get; set; }

        /// <summary>
        /// The content of the post.
        /// </summary>
        [XmlElement]
        public string Content { get; set; }

        /// <summary>
        /// A unique string that identifies the post.
        /// </summary>
        [XmlElement]
        public string Slug { get; set; }

        /// <summary>
        /// A list of tags associated with the post.
        /// </summary>
        [XmlArray]
        public List<Tag> Tags { get; set; }

        /// <summary>
        /// Creation date and time.
        /// </summary>
        [XmlElement]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Date and time of the last edit.
        /// </summary>
        [XmlElement]
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Determines if the post is published and public to everyone or not.
        /// </summary>
        [XmlElement]
        public bool IsPublished { get; set; }

        /// <summary>
        /// Title of the previous post according to the creation dates.
        /// </summary>
        [XmlElement]
        public string PreviousPostTitle { get; set; }

        /// <summary>
        /// Slug of the previous post according to the creation dates.
        /// </summary>
        [XmlElement]
        public string PreviousPostSlug { get; set; }

        /// <summary>
        /// Title of the next post according to the creation dates.
        /// </summary>
        [XmlElement]
        public string NextPostTitle { get; set; }

        /// <summary>
        /// Slug of the next post according to the creation dates.
        /// </summary>
        [XmlElement]
        public string NextPostSlug { get; set; }

        /// <summary>
        /// Returns a string that represents the current post.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Id}: {Title}";
        }
    }
}
