using Brugnner.API.Core.Resources;
using Brugnner.API.Core.Resources.Post;
using System;
using System.Collections.Generic;

namespace Brugnner.API.Core.Contracts.Services
{
    /// <summary>
    /// Represents a service in charge of handling posts.
    /// </summary>
    public interface IPostService
    {
        /// <summary>
        /// Returns a list of posts.
        /// </summary>
        /// <param name="listParams">Search and pagination parameters.</param>
        /// <returns></returns>
        ListResultResource<PostResource> List(ListParamsResource listParams);

        /// <summary>
        /// Returns a list of post that contain the specified tag.
        /// </summary>
        /// <param name="tag">Tag name.</param>
        /// <param name="listParams">Search and pagination parameters.</param>
        /// <returns></returns>
        ListResultResource<PostResource> ListByTag(string tag, ListParamsResource listParams);

        /// <summary>
        /// Creates a post.
        /// </summary>
        /// <param name="postResource">Post to create.</param>
        /// <returns></returns>
        PostResource Create(CreatePostResource postResource);

        /// <summary>
        /// Updates a post.
        /// </summary>
        /// <param name="postResource">Post to update.</param>
        /// <returns></returns>
        PostResource Update(UpdatePostResource postResource);

        /// <summary>
        /// Returns a post searching by slug.
        /// </summary>
        /// <param name="slug">Slug of the post.</param>
        /// <returns></returns>
        PostResource GetBySlug(string slug);

        /// <summary>
        /// Returns a post searching by id.
        /// </summary>
        /// <param name="id">Id of the post.</param>
        /// <returns></returns>
        PostResource GetById(Guid id);

        /// <summary>
        /// Changes a post state from published to unpublished and viceversa.
        /// </summary>
        /// <param name="id">Post Id.</param>
        /// <returns></returns>
        PostResource TogglePublication(Guid id);

        /// <summary>
        /// Removes a post.
        /// </summary>
        /// <param name="id">Post Id.</param>
        /// <returns></returns>
        void Delete(Guid id);

        /// <summary>
        /// Returns a list of the posts including those which are unpublished.
        /// </summary>
        /// <param name="listParams">Search and pagination parameters.</param>
        /// <returns></returns>
        ListResultResource<PostResource> FullList(ListParamsResource listParams);

        /// <summary>
        /// Returns a list of all tags.
        /// </summary>
        /// <returns></returns>
        List<TagResource> GetTags();

        /// <summary>
        /// Returns a random post.
        /// </summary>
        /// <returns></returns>
        PostResource GetRandomPost();

        /// <summary>
        /// Returns the latest post.
        /// </summary>
        /// <returns></returns>
        PostResource GetLatestPost();
    }
}
