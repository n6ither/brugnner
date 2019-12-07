using AutoMapper;
using Brugnner.API.Core.Contracts.Repositories;
using Brugnner.API.Core.Contracts.Services;
using Brugnner.API.Core.Domain;
using Brugnner.API.Core.Exceptions;
using Brugnner.API.Core.Extensions;
using Brugnner.API.Core.Resources;
using Brugnner.API.Core.Resources.Post;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Brugnner.API.Core.Services
{
    /// <summary>
    /// The blog posts service.
    /// </summary>
    public class PostService : BaseService<Post, Guid, PostResource>, IPostService
    {
        private readonly IPostRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<PostService> _logger;
        private readonly Random _random = new Random();

        /// <summary>
        /// Creates a new instance of the service.
        /// </summary>
        /// <param name="repository">Post repository.</param>
        /// <param name="mapper">Mapper.</param>
        /// <param name="logger">Logger.</param>
        public PostService(IPostRepository repository, IMapper mapper, ILogger<PostService> logger)
        {
            _repository = repository.ThrowIfNull(nameof(repository));
            _mapper = mapper.ThrowIfNull(nameof(mapper));
            _logger = logger.ThrowIfNull(nameof(logger));
        }

        #region IPostService
        /// <summary>
        /// Creates a post.
        /// </summary>
        /// <param name="postResource">Post to create.</param>
        /// <returns></returns>
        public PostResource Create(CreatePostResource postResource)
        {
            var post = _mapper.Map<Post>(postResource);
            post.Id = Guid.NewGuid();
            post.Slug = post.Title.ToSlug();
            post.CreatedAt = DateTime.Now;

            Repository().Save(post);

            return _mapper.Map<PostResource>(post);
        }

        /// <summary>
        /// Updates a post.
        /// </summary>
        /// <param name="postResource">Post to update.</param>
        /// <returns></returns>
        public PostResource Update(UpdatePostResource postResource)
        {
            Post post = Repository().GetOne(postResource.Id);

            if (post == null)
            {
                throw new ResourceNotFoundException($"Post {postResource.Id} not found");
            }

            post = _mapper.Map(postResource, post);

            post.Slug = post.Title.ToSlug();
            post.UpdatedAt = DateTime.Now;

            Repository().Save(post);

            return _mapper.Map<PostResource>(post);
        }

        /// <summary>
        /// Returns a post searching by slug.
        /// </summary>
        /// <param name="slug">Slug of the post.</param>
        /// <returns></returns>
        public PostResource GetBySlug(string slug)
        {
            var post = Repository().GetOne(x => x.Slug.Equals(slug));

            if (post != null)
            {
                SetRelatedPosts(post);
            }

            return _mapper.Map<PostResource>(post);
        }

        /// <summary>
        /// Returns a post searching by id.
        /// </summary>
        /// <param name="id">Id of the post.</param>
        /// <returns></returns>
        public PostResource GetById(Guid id)
        {
            var post = Repository().GetOne(id);

            if (post == null)
            {
                throw new ResourceNotFoundException($"Post {id} not found");
            }

            return _mapper.Map<PostResource>(post);
        }

        /// <summary>
        /// Changes a post state from published to unpublished and viceversa.
        /// </summary>
        /// <param name="id">Post Id.</param>
        /// <returns></returns>
        public PostResource TogglePublication(Guid id)
        {
            var post = Repository().GetOne(x => x.Id.Equals(id));
            post.IsPublished = !post.IsPublished;

            Repository().Save(post);

            return _mapper.Map<PostResource>(post);
        }

        /// <summary>
        /// Removes a post.
        /// </summary>
        /// <param name="id">Post Id.</param>
        public void Delete(Guid id)
        {
            var post = Repository().GetOne(id);

            if (post == null)
            {
                throw new ResourceNotFoundException($"Post {id} not found");
            }

            Repository().Delete(id);
        }

        /// <summary>
        /// Returns a list of the posts including those which are unpublished.
        /// </summary>
        /// <param name="listParams">Search and pagination parameters.</param>
        /// <returns></returns>
        public ListResultResource<PostResource> FullList(ListParamsResource listParams)
        {
            return _repository.GetAllAsAdmin().MapToListResultResource<Post, Guid, PostResource>(listParams, _mapper);
        }

        /// <summary>
        /// Returns a list of all tags.
        /// </summary>
        /// <returns></returns>
        public List<TagResource> GetTags()
        {
            IEnumerable<Tag> tags = Repository()
                .GetAll()
                .SelectMany(x => x.Tags)
                .GroupBy(x => x.Name)
                .Select(x => new Tag(x.Key));

            return _mapper.Map<List<TagResource>>(tags);
        }

        /// <summary>
        /// Returns a list of post that contain the specified tag.
        /// </summary>
        /// <param name="tag">Tag name.</param>
        /// <param name="listParams">Search and pagination parameters.</param>
        /// <returns></returns>
        public ListResultResource<PostResource> ListByTag(string tag, ListParamsResource listParams)
        {
            return Repository().GetAll().Where(p => p.Tags.Any(t => t.Name.Equals(tag)))
                .MapToListResultResource<Post, Guid, PostResource>(listParams, _mapper);
        }

        /// <summary>
        /// Returns a random post.
        /// </summary>
        /// <returns></returns>
        public PostResource GetRandomPost()
        {
            var posts = Repository().GetAll().ToList();
            var post = posts[_random.Next(posts.Count)];

            SetRelatedPosts(post);

            return _mapper.Map<PostResource>(post);
        }

        /// <summary>
        /// Returns the latest post.
        /// </summary>
        /// <returns></returns>
        public PostResource GetLatestPost()
        {
            var post = Repository().GetAll().OrderByDescending(x => x.CreatedAt).FirstOrDefault();

            return _mapper.Map<PostResource>(post);
        }
        #endregion

        #region BaseService
        /// <summary>
        /// Returns a list of posts.
        /// </summary>
        /// <param name="listParams">Search and pagination parameters.</param>
        /// <returns></returns>
        public override ListResultResource<PostResource> List(ListParamsResource listParams)
        {
            return Repository().GetAll().MapToListResultResource<Post, Guid, PostResource>(listParams, _mapper);
        }

        /// <summary>
        /// Returns the entity repository.
        /// </summary>
        /// <returns></returns>
        public override IRepository<Post, Guid> Repository()
        {
            return _repository;
        }
        #endregion

        private void SetRelatedPosts(Post post)
        {
            if (post == null)
                return;

            var previous = GetPreviousPost(post.CreatedAt);
            post.PreviousPostTitle = previous?.Title;
            post.PreviousPostSlug = previous?.Slug;

            var next = GetNextPost(post.CreatedAt);
            post.NextPostTitle = next?.Title;
            post.NextPostSlug = next?.Slug;
        }

        private Post GetNextPost(DateTime createdAt)
        {
            return Repository().GetAll().Where(x => x.CreatedAt > createdAt).OrderBy(x => x.CreatedAt).FirstOrDefault();
        }

        private Post GetPreviousPost(DateTime createdAt)
        {
            return Repository().GetAll().Where(x => x.CreatedAt < createdAt).OrderBy(x => x.CreatedAt).LastOrDefault();
        }
    }
}
