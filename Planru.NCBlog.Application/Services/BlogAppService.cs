using Planru.NCBlog.Application.Interfaces;
using Planru.NCBlog.Domain.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace Planru.NCBlog.Application.Services
{
    public class BlogAppService : IBlogAppService
    {
        private readonly IBlogRepository _blogRepository;

        public BlogAppService(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }
    }
}
