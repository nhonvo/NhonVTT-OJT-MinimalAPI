using Microsoft.EntityFrameworkCore;

#nullable disable
namespace NhonVTT_OJT_MinimalAPI
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> context) : base(context)
        {
            Database.Migrate();
        }
        public DbSet<Book> Books { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<Book>().HasKey(x => x.Id);
        }
    }
}