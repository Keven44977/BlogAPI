using Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class BlogContext:DbContext
    {
        public BlogContext()
        {

        }

        public BlogContext(DbContextOptions<BlogContext> options)
           : base(options)
        {

        }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<Post>? Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql(DatabaseConnexion.GetConnexionString());
    }
}