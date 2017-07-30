using Planru.NCBlog.Domain.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using Planru.NCBlog.Domain.Models;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Planru.NCBlog.Persistence.EFCore
{
    public class BlogRepository : Repository<Blog>, IBlogRepository
    {
        public BlogRepository(IContentDbContext dbContext) : base(dbContext)
        {

        }
    }
}
