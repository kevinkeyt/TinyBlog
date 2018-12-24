using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            var post = new Post
            {
                Title = model.Title,
                Author = model.Author,
                Slug = model.Slug,
                Excerpt = model.Excerpt,
                Content = model.Content
            };
            post.SetPubDate(model.PubDate);
            foreach (var category in model.PostCategories)
                post.AddCategory(category);
            postRepository.Add(post);
            return mapper.Map<PostViewModel>(post);
        }

        public async Task<IEnumerable<PostViewModel>> GetAll()
        {
            var posts = await postRepository.ListAll();
            return posts.Select(m => mapper.Map<PostViewModel>(m));
        }

        public PostViewModel GetById(string id)
        {
            return null;// mapper.Map<PostViewModel>(postRepository.GetById(id));
        }

        public async Task<Dictionary<string, int>> GetCategories()
        {
            return await postRepository.GetCategories();
        }

        public PostViewModel GetPostBySlug(string slug, bool IsAdmin)
        {
            return mapper.Map<PostViewModel>(postRepository.GetPostBySlug(slug, IsAdmin));
        }

        public async Task<IEnumerable<PostViewModel>> GetPostsByCategory(string category)
        {
            var posts = await postRepository.GetPostsByCategory(category);
            return posts.Select(m => mapper.Map<PostViewModel>(m));
        }

        public async Task<IEnumerable<PostViewModel>> GetPublicPosts()
        {
            var posts = await postRepository.GetPublicPosts();
            return posts.Select(m => mapper.Map<PostViewModel>(m));
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
