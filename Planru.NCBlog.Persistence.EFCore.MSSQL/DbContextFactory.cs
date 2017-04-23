using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Planru.NCBlog.Persistence.EFCore.MSSQL
{
    public class DbContextFactory : IDbContextFactory<ContentDbContext>
    {
        public ContentDbContext Create(DbContextFactoryOptions options)
        {
            var builder = new DbContextOptionsBuilder<ContentDbContext>();
            builder.UseSqlServer("Server=(local)\\SQLEXPRESS;Database=NBlog;Trusted_Connection=True;MultipleActiveResultSets=true");
            return new ContentDbContext(builder.Options);
        }
    }
}
