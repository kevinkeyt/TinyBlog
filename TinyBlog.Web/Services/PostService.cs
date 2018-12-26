using AutoMapper;
using System;
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

        public async Task<PostViewModel> Add(PostViewModel model)
        {
            var post = new Post
            {
                Title = model.Title,
                Author = model.Author,
                Slug = model.Slug,
                Excerpt = model.Excerpt,
                Content = model.Content,
                LastModified = DateTime.UtcNow
            };
            post.SetPubDate(model.PubDate);
            foreach (var category in model.GetPostCategories())
                post.AddCategory(category);
            await postRepository.Add(post);
            return mapper.Map<PostViewModel>(post);
        }

        public async Task<IEnumerable<PostViewModel>> GetAll()
        {
            var posts = await postRepository.ListAll();
            return posts.Select(m => mapper.Map<PostViewModel>(m));
        }

        public async Task<PostViewModel> GetById(string id, string partitionKey)
        {
            var post = await postRepository.GetById(id, partitionKey);
            return mapper.Map<PostViewModel>(post);
        }

        public async Task<Dictionary<string, int>> GetCategories()
        {
            return await postRepository.GetCategories();
        }

        public async Task<PostViewModel> GetPostBySlug(string slug)
        {
            var post = await postRepository.GetPostBySlug(slug);
            return mapper.Map<PostViewModel>(post);
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

        public async Task Update(PostViewModel model)
        {
            var post = mapper.Map<Post>(model);
            post.SetPubDate(model.PubDate);
            foreach (var category in model.GetPostCategories())
                post.AddCategory(category);
            await postRepository.Update(post);
        }
    }
}
