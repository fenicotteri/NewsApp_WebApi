using AutoMapper;
using NewsApp.Dto;
using NewsApp.Models;

namespace NewsApp.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, PublicUserDto>();
            CreateMap<User, PublicUserWithRatingDto>();
            CreateMap<User, AuthOutputDto>();
            CreateMap<User, SingupDto>();
            CreateMap<SingupDto, User>();

            CreateMap<Post, PostOutputDto>();
            CreateMap<Post, PostOutputDto>()
               .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating.Value))
               .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.PostTags.Select(postTag => postTag.Tag)));

            CreateMap<Tag, TagOutputDto>();
            CreateMap<AddTagDto, Tag>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.tag));
           
            CreateMap<Comment, CommentOutputDto>();
            CreateMap<CreateCommentDto, Comment>();

        }
    }
}
