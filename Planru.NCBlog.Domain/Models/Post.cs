using Planru.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Planru.NCBlog.Domain.Models
{
    public class Post : Entity
    {
        public List<Comment> Comments { get; }
        public Blog Blog { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
