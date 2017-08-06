using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Planru.NCBlog.Persistence.EFCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Planru.NCMS.Persistence.EFCore
{
    //public class DbContextFactory : IDbContextFactory<ContentDbContext>
    //{
    //    public ContentDbContext Create(DbContextFactoryOptions options)
    //    {
    //        var builder = new DbContextOptionsBuilder<ContentDbContext>();
    //        builder.UseSqlServer("Server=(local)\\SQLEXPRESS;Database=NCMS;Trusted_Connection=True;MultipleActiveResultSets=true");
    //        return new ContentDbContext(builder.Options);
    //    }
    //}
}
