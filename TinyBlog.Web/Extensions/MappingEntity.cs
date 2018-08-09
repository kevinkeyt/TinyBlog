using AutoMapper;
using TinyBlog.Core.Entities;
using TinyBlog.Web.ViewModels;

namespace TinyBlog.Web.Extensions
{
    public class MappingEntity : Profile
    {
        public MappingEntity()
        {
            CreateMap<BlogViewModel, Blog>();
            CreateMap<Blog, BlogViewModel>();
            CreateMap<PostViewModel, Post>();
            CreateMap<Post, PostViewModel>();
        }
    }
}
