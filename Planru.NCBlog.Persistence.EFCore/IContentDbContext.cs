using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Planru.Domain.Core.Models;
using Planru.NCBlog.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Planru.NCBlog.Persistence.EFCore
{
    public interface IContentDbContext : IDisposable
    {
        DbSet<Blog> Blogs { get; set; }
        DbSet<Post> Posts { get; set; }
        DbSet<Comment> Comments { get; set; }
        DbSet<Category> Categories { get; set; }
        ChangeTracker ChangeTracker { get; }
        DatabaseFacade Database { get; }
        DbSet<TEntity> Set<TEntity>() where TEntity: class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
