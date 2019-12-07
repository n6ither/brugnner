using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Brugnner.API.Core.Resources.Post
{
    /// <summary>
    /// Represents a post to be updated.
    /// </summary>
    public class UpdatePostResource
    {
        /// <summary>
        /// Id of the post.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Title of the post.
        /// </summary>
        [Required, StringLength(maximumLength: 150, MinimumLength = 10, ErrorMessage = "Length must be between 10 and 150 characters.")]
        public string Title { get; set; }

        /// <summary>
        /// Short description of the post.
        /// </summary>
        [Required, StringLength(maximumLength: 200, MinimumLength = 10, ErrorMessage = "Length must be between 10 and 200 characters.")]
        public string Excerpt { get; set; }

        /// <summary>
        /// Content of the post.
        /// </summary>
        [Required, StringLength(maximumLength: 10000, MinimumLength = 10, ErrorMessage = "Length must be between 10 and 10.000 characters.")]
        public string Content { get; set; }

        /// <summary>
        /// Tags of the post.
        /// </summary>
        [Required]
        public List<TagResource> Tags { get; set; }

        /// <summary>
        /// Specify if the post is published or not.
        /// </summary>
        public bool IsPublished { get; set; }
    }
}
