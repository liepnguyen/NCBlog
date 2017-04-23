using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Planru.NCBlog.Persistence.EFCore.MSSQL;

namespace Planru.NCBlog.Persistence.EFCore.MSSQL.Migrations
{
    [DbContext(typeof(ContentDbContext))]
    partial class ContentDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Planru.NCBlog.Domain.Models.Blog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Url")
                        .HasMaxLength(2083);

                    b.HasKey("Id");

                    b.ToTable("Blogs");

                    b.HasAnnotation("SqlServer:TableName", "Blogs");
                });

            modelBuilder.Entity("Planru.NCBlog.Domain.Models.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("CategoryId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Planru.NCBlog.Domain.Models.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<Guid?>("PostId");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Planru.NCBlog.Domain.Models.Post", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("BlogId");

                    b.Property<string>("Content");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.HasIndex("BlogId");

                    b.ToTable("Posts");

                    b.HasAnnotation("SqlServer:TableName", "Posts");
                });

            modelBuilder.Entity("Planru.NCBlog.Domain.Models.Category", b =>
                {
                    b.HasOne("Planru.NCBlog.Domain.Models.Category")
                        .WithMany("SubCaterogies")
                        .HasForeignKey("CategoryId");
                });

            modelBuilder.Entity("Planru.NCBlog.Domain.Models.Comment", b =>
                {
                    b.HasOne("Planru.NCBlog.Domain.Models.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId");
                });

            modelBuilder.Entity("Planru.NCBlog.Domain.Models.Post", b =>
                {
                    b.HasOne("Planru.NCBlog.Domain.Models.Blog", "Blog")
                        .WithMany("Post")
                        .HasForeignKey("BlogId");
                });
        }
    }
}
