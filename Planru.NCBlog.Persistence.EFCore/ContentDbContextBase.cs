using Microsoft.EntityFrameworkCore;
using Planru.CrossCutting.Identity;
using Planru.NCBlog.Domain.Models;

namespace Planru.NCBlog.Persistence.EFCore
{
    public class ContentDbContextBase : CustomIdentityDbContext, IContentDbContext
    {
        public ContentDbContextBase(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
