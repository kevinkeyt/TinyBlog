using System;
using TinyBlog.Core.SharedKernel;

namespace TinyBlog.Core.Entities
{
    public class Comment : BaseEntity
    {
        public Comment(string author, string email, string content) : this()
        {
            Author = author;
            Email = email;
            Content = content;
        }

        private Comment()
        {
            PostedDate = DateTime.UtcNow;
        }

        public string Author { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Content { get; set; }
        public DateTime PostedDate { get; private set; }
        public string Ip { get; set; }
        public string UserAgent { get; set; }
        public bool IsAdmin { get; private set; }
        public bool IsApproved { get; private set; }

        public void Approve(bool isApproved)
        {
            IsApproved = isApproved;
        }

        public void UnApprove()
        {
            IsApproved = false;
        }

        public void MarkAsAdmin(bool isAdmin)
        {
            IsAdmin = isAdmin;
        }
    }
}
