using Planru.Domain.Core.Persistence;
using Planru.NCBlog.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Planru.NCBlog.Domain.Persistence
{
    public interface IPostRepository : IRepository<Post>
    {
    }
}
