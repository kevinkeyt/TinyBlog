using System.Collections.Generic;
using TinyBlog.Domain;

namespace TinyBlog.Data
{
    public interface IDataContext
    {
        List<Post> GetAllPosts();
        IEnumerable<Post> GetPublicPosts();
        IEnumerable<Post> GetPostsByCategory(string category);
        Post GetPostBySlug(string Slug);
        Post GetPostById(string Id);
        void SavePost(Post post);
        void DeletePost(Post post);
        Dictionary<string, int> GetCategories();

        Blog GetBlogInfo();
        void SaveBlogInfo(Blog blog);
    }
}
