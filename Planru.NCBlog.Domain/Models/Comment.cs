using Planru.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Planru.NCBlog.Domain.Models
{
    public class Comment : Entity
    {
        public string Content { get; set; }
    }
}
