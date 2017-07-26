using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Planru.CrossCutting.Identity.Models;

namespace Planru.CrossCutting.Identity
{
    public class CustomIdentityDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public CustomIdentityDbContext(DbContextOptions options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
