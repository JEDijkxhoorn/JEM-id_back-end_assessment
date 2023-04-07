using JEM_id_assessment.Models;
using Microsoft.EntityFrameworkCore;

namespace JEM_id_assessment.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<ProductGroup> ProductGroups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*base.OnModelCreating(modelBuilder);*/
            modelBuilder.Entity<Article>()
                .Property(a => a.CodeId).IsRequired().HasMaxLength(13);
            modelBuilder.Entity<Article>()
                .Property(a => a.Name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Article>().HasIndex(a => a.CodeId).IsUnique();
        }
    }
}
