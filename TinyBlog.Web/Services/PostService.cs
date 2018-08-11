using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using TinyBlog.Core.Entities;
using TinyBlog.Core.Interfaces;
using TinyBlog.Web.Interfaces;
using TinyBlog.Web.ViewModels;

namespace TinyBlog.Web.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository postRepository;
        private readonly IMapper mapper;

        public PostService(IPostRepository postRepository, IMapper mapper)
        {
            this.postRepository = postRepository;
            this.mapper = mapper;
        }

        public PostViewModel Add(PostViewModel model)
        {
            var post = new Post(model.Title, model.Author, model.Slug)
            {
                Excerpt = model.Excerpt,
                Content = model.Content
            };
            post.SetPubDate(model.PubDate);
            foreach (var category in model.PostCategories)
                post.AddCategory(category);
            postRepository.Add(post);
            return mapper.Map<PostViewModel>(post);
        }

        public IEnumerable<PostViewModel> GetAll()
        {
            return postRepository.ListAll().Select(m => mapper.Map<PostViewModel>(m));
        }

        public PostViewModel GetById(string id)
        {
            return mapper.Map<PostViewModel>(postRepository.GetById(id));
        }

        public Dictionary<string, int> GetCategories()
        {
            return postRepository.GetCategories();
        }

        public PostViewModel GetPostBySlug(string slug, bool IsAdmin)
        {
            return mapper.Map<PostViewModel>(postRepository.GetPostBySlug(slug, IsAdmin));
        }

        public IEnumerable<PostViewModel> GetPostsByCategory(string category)
        {
            return postRepository.GetPostsByCategory(category).Select(m => mapper.Map<PostViewModel>(m));
        }

        public IEnumerable<PostViewModel> GetPublicPosts()
        {
            return postRepository.GetPublicPosts().Select(m => mapper.Map<PostViewModel>(m));
        }

        public void Update(PostViewModel model)
        {
            var post = mapper.Map<Post>(model);
            post.SetPubDate(model.PubDate);
            foreach (var category in model.PostCategories)
                post.AddCategory(category);
            postRepository.Update(post);
        }
    }
}
