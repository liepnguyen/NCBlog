using Planru.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Planru.NCBlog.Domain.Models
{
    public class Category : Entity
    {
        public string Name { get; set; }
        public List<Category> SubCaterogies { get; set; }
    }
}
