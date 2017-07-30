using System;
using System.Linq;

namespace Planru.CrossCutting.Identity.Web
{
    public class PermissionViewModel
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string GroupName { get; set; }
        public string Description { get; set; }
    }
}
