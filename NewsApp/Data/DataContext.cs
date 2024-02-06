using Microsoft.EntityFrameworkCore;
using NewsApp.Models;
using System.Reflection.Metadata;

namespace NewsApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Tag> Tags { get; set; }    
        public DbSet<PostTag> PostTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<PostTag>()
                .HasKey(pt => new { pt.PostId, pt.TagId });
            modelBuilder.Entity<PostTag>()
                .HasOne(p => p.Post)
                .WithMany(pt => pt.PostTags)
                .HasForeignKey(p => p.PostId);
            modelBuilder.Entity<PostTag>()
                .HasOne(t => t.Tag)
                .WithMany(pt => pt.PostTags)
                .HasForeignKey(t => t.TagId);


            modelBuilder.Entity<Comment>()
                .ToTable(tb => tb.HasTrigger("SetUpdatedAt"));
        }

    }
}
