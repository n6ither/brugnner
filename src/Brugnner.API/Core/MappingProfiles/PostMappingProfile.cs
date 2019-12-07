using AutoMapper;
using Brugnner.API.Core.Domain;
using Brugnner.API.Core.Resources.Post;

namespace Brugnner.API.Core.MappingProfiles
{
    class PostMappingProfile : Profile
    {
        public PostMappingProfile()
        {
            CreateMap<Post, PostResource>()
                .ForMember(x => x.Id, x => x.MapFrom(k => k.Id))
                .ForMember(x => x.Title, x => x.MapFrom(k => k.Title))
                .ForMember(x => x.Excerpt, x => x.MapFrom(k => k.Excerpt))
                .ForMember(x => x.Content, x => x.MapFrom(k => k.Content))
                .ForMember(x => x.Tags, x => x.MapFrom(k => k.Tags))
                .ForMember(x => x.CreatedAt, x => x.MapFrom(k => k.CreatedAt))
                .ForMember(x => x.UpdatedAt, x => x.MapFrom(k => k.UpdatedAt))
                .ForMember(x => x.PreviousPostTitle, x => x.MapFrom(k => k.PreviousPostTitle))
                .ForMember(x => x.PreviousPostSlug, x => x.MapFrom(k => k.PreviousPostSlug))
                .ForMember(x => x.NextPostTitle, x => x.MapFrom(k => k.NextPostTitle))
                .ForMember(x => x.NextPostSlug, x => x.MapFrom(k => k.NextPostSlug));

            CreateMap<UpdatePostResource, Post>()
                .ForMember(x => x.Id, x => x.MapFrom(k => k.Id))
                .ForMember(x => x.Title, x => x.MapFrom(k => k.Title))
                .ForMember(x => x.Excerpt, x => x.MapFrom(k => k.Excerpt))
                .ForMember(x => x.Content, x => x.MapFrom(k => k.Content))
                .ForMember(x => x.Tags, x => x.MapFrom(k => k.Tags))
                .ForMember(x => x.IsPublished, x => x.MapFrom(k => k.IsPublished));

            CreateMap<CreatePostResource, Post>()
               .ForMember(x => x.Title, x => x.MapFrom(k => k.Title))
               .ForMember(x => x.Excerpt, x => x.MapFrom(k => k.Excerpt))
               .ForMember(x => x.Content, x => x.MapFrom(k => k.Content))
               .ForMember(x => x.Tags, x => x.MapFrom(k => k.Tags))
               .ForMember(x => x.IsPublished, x => x.MapFrom(k => k.IsPublished));

            CreateMap<Tag, TagResource>();
            CreateMap<TagResource, Tag>();
        }
    }
}
