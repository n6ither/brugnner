using System;
using System.Collections.Generic;

namespace Brugnner.API.Core.Resources.Post
{
    /// <summary>
    /// A blog post.
    /// </summary>
    public class PostResource : APIResource
    {
        /// <summary>
        /// Id of the post.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Title of the blog.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// A little description of the post.
        /// </summary>
        public string Excerpt { get; set; }

        /// <summary>
        /// The content of the post.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// A unique string that identifies the post.
        /// </summary>
        public string Slug { get; set; }

        /// <summary>
        /// A list of tags associated with the post.
        /// </summary>
        public List<TagResource> Tags { get; set; }

        /// <summary>
        /// Creation date and time.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Date and time of the last edit.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Determines if the post is published and public to everyone or not.
        /// </summary>
        public bool IsPublished { get; set; }

        /// <summary>
        /// Title of the previous post according to the creation dates.
        /// </summary>
        public string PreviousPostTitle { get; internal set; }

        /// <summary>
        /// Slug of the previous post according to the creation dates.
        /// </summary>
        public string PreviousPostSlug { get; set; }

        /// <summary>
        /// Title of the next post according to the creation dates.
        /// </summary>
        public string NextPostTitle { get; internal set; }

        /// <summary>
        /// Slug of the next post according to the creation dates.
        /// </summary>
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
