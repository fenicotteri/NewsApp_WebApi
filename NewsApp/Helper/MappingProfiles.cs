using AutoMapper;
using Microsoft.AspNetCore.Identity;
using NewsApp.Dto;
using NewsApp.Models;

namespace NewsApp.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, PublicUserDto>();
            CreateMap<User, AuthOutputDto>();
            CreateMap<User, SingupDto>();
            CreateMap<IdentityResult, PublicUserDto>();
            CreateMap<SingupDto, User>();

            CreateMap<Post, PostOutputDto>();
            CreateMap<List<Post>, PostOutputWithAuthorDto>()
                .ForMember(dest => dest.Posts, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Count));

            CreateMap<Post, PostOutputWithAuthorDto>();
            CreateMap<Post, PostOutputDto>();
               //.ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.PostTags.Select(postTag => postTag.Tag)));

            CreateMap<Tag, TagOutputDto>();
            CreateMap<AddTagDto, Tag>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Tag));
           
            CreateMap<Comment, CommentOutputDto>();
            CreateMap<CreateCommentDto, Comment>();
            CreateMap<CreatePostDto, Post>();

        }
    }
}
