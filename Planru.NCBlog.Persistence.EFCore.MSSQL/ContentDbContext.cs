using System;
using Microsoft.EntityFrameworkCore;
using Planru.NCBlog.Domain.Models;

namespace Planru.NCBlog.Persistence.EFCore.MSSQL
{
    public class ContentDbContext : ContentDbContextBase
    {
        public ContentDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>(entity =>
            {
                entity.ForSqlServerToTable("Blogs");

                entity.HasKey(p => p.Id);

                entity.Property(p => p.Url)
                .HasMaxLength(2083);
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ForSqlServerToTable("Posts");

                entity.HasKey(p => p.Id);

                entity.Property(p => p.Title)
                .HasMaxLength(255)
                .IsRequired();

                entity.Property(p => p.Content);

                entity.Property(p => p.Slug)
                .HasMaxLength(255)
                .IsRequired();

                entity.HasMany(p => p.Comments)
                .WithOne(p => p.Post);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
