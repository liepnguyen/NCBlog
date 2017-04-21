using System;
using Microsoft.EntityFrameworkCore;

namespace Planru.NCBlog.Persistence.EFCore.MSSQL
{
    public class ContentDbContext : ContentDbContextBase
    {
        public ContentDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
