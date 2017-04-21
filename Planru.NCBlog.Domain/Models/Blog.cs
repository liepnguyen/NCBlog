using Planru.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Planru.NCBlog.Domain.Models
{
    public class Blog : Entity
    {
        public string Url { get; set; }
        public List<Post> Post { get; set; }
    }
}
