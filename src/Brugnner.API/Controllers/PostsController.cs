using Brugnner.API.Core.Contracts.Services;
using Brugnner.API.Core.Exceptions;
using Brugnner.API.Core.Extensions;
using Brugnner.API.Core.Resources;
using Brugnner.API.Core.Resources.Post;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace Brugnner.API.Controllers
{
    /// <summary>
    /// Posts endpoint.
    /// </summary>
    [Route("api/posts")]
    public class PostsController : BrugnnerController
    {
        private readonly IPostService _postService;
        private readonly ILogger<PostsController> _logger;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="postService">Post service.</param>
        /// <param name="logger">Logger.</param>
        public PostsController(IPostService postService, ILogger<PostsController> logger)
        {
            _postService = postService.ThrowIfNull(nameof(postService));
            _logger = logger.ThrowIfNull(nameof(logger));
        }

        /// <summary>
        /// Returns a list posts.
        /// </summary>
        /// <param name="listParams">Search and pagination parameters.</param>
        /// <returns></returns>
        [HttpGet()]
        [AllowAnonymous]
        public IActionResult List([FromQuery]ListParamsResource listParams)
        {
            var posts = _postService.List(listParams);

            return Ok(posts);
        }

        /// <summary>
        /// Returns a list posts that contains the specified tag.
        /// </summary>
        /// <param name="tag">Tag name to filter by.</param>
        /// <param name="listParams">Search and pagination parameters.</param>
        /// <returns></returns>
        [HttpGet("tag/{tag}")]
        [AllowAnonymous]
        public IActionResult ListByTag(string tag, [FromQuery]ListParamsResource listParams)
        {
            listParams.Filters = $@"Tags.Any(x => x.Name.Equals(""{tag}""))";
            var posts = _postService.List(listParams);

            return Ok(posts);
        }

        /// <summary>
        /// Returns the full version of the post list including those which are unpublished.
        /// </summary>
        /// <param name="listParams">Search and pagination parameters.</param>
        /// <returns></returns>
        [HttpGet("full")]
        public IActionResult FullList([FromQuery]ListParamsResource listParams)
        {
            var posts = _postService.FullList(listParams);

            return Ok(posts);
        }

        /// <summary>
        /// Gets a single post by slug.
        /// </summary>
        /// <param name="slug">Post slug</param>
        /// <response code="200">The post.</response>
        /// <response code="404">Post not found.</response>
        /// <response code="500">Something went wrong.</response>
        /// <returns></returns>
        [HttpGet("slug/{slug}")]
        [AllowAnonymous]
        public IActionResult GetBySlug(string slug)
        {
            var post = _postService.GetBySlug(slug);

            if (post == null)
            {
                return NotFound($"Post '{slug}' not found");
            }

            return Ok(post);
        }

        /// <summary>
        /// Gets a random post.
        /// </summary>
        /// <response code="200">The post.</response>
        /// <response code="500">Something went wrong.</response>
        /// <returns></returns>
        [HttpGet("random")]
        [AllowAnonymous]
        public IActionResult GetRandom()
        {
            var post = _postService.GetRandomPost();

            return Ok(post);
        }

        /// <summary>
        /// Gets the last post.
        /// </summary>
        /// <response code="200">The post.</response>
        /// <response code="500">Something went wrong.</response>
        /// <returns></returns>
        [HttpGet("latest")]
        [AllowAnonymous]
        public IActionResult GetLatest()
        {
            var post = _postService.GetLatestPost();

            return Ok(post);
        }

        /// <summary>
        /// Get all tags.
        /// </summary>
        /// <returns></returns>
        [HttpGet("tags")]
        [AllowAnonymous]
        public IActionResult GetTags()
        {
            var tags = _postService.GetTags();

            return Ok(tags);
        }

        /// <summary>
        /// Creates a new post.
        /// </summary>
        /// <param name="post">Post to create.</param>
        /// <returns>Created post.</returns>
        /// <response code="200">Created post.</response>
        /// <response code="400">You're not authorized to create a post.</response>
        /// <response code="500">Something went wrong.</response>
        [HttpPost()]
        public IActionResult Create([FromBody]CreatePostResource post)
        {
            var result = _postService.Create(post);

            return Ok(result);
        }

        /// <summary>
        /// Updates a post.
        /// </summary>
        /// <param name="post">Post to update.</param>
        /// <returns>Updated post.</returns>
        /// <response code="200">Updated post.</response>
        /// <response code="400">You're not authorized to update a post.</response>
        /// <response code="500">Something went wrong.</response>
        [HttpPut()]
        public IActionResult Update([FromBody]UpdatePostResource post)
        {
            var result = _postService.Update(post);

            return Ok(result);
        }

        /// <summary>
        /// Gets a single post by Id.
        /// </summary>
        /// <param name="id">Id of the post.</param>
        /// <response code="200">The post.</response>
        /// <response code="404">Post not found.</response>
        /// <response code="500">Something went wrong.</response>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var post = _postService.GetById(id);

            if (post == null)
            {
                throw new ResourceNotFoundException($"Post {id} not found");
            }

            return Ok(post);
        }

        /// <summary>
        /// Toggles post status between published and not published.
        /// </summary>
        /// <param name="resource">Post Id.</param>
        /// <returns></returns>
        [HttpPut("togglepublication")]
        public IActionResult TogglePublication([FromBody]IdPlaceholderResource<Guid> resource)
        {
            var result = _postService.TogglePublication(resource.Id);

            return Ok(result);
        }

        /// <summary>
        /// Deletes a post.
        /// </summary>
        /// <param name="id">Post Id.</param>
        /// <response code="204">Post deleted.</response>
        /// <response code="400">You're not authorized to delete a post.</response>
        /// <response code="500">Something went wrong.</response>
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _postService.Delete(id);

            return Ok($"Post {id} deleted");
        }
    }
}
