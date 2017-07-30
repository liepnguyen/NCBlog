using Microsoft.EntityFrameworkCore;
using Planru.CrossCutting.Identity;
using Planru.NCBlog.Domain.Models;

namespace Planru.NCBlog.Persistence.EFCore
{
    public class ContentDbContext : CustomIdentityDbContext, IContentDbContext
    {
        public ContentDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
