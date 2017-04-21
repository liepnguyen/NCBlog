using Microsoft.EntityFrameworkCore;
using Planru.NCBlog.Domain.Models;
using System;

namespace Planru.NCBlog.Persistence.EFCore
{
    public class ContentDbContextBase : DbContext, IContentDbContext
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
