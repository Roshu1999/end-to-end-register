using Microsoft.EntityFrameworkCore;

namespace IdentityCMS.Models
{
    public class CustomDbContext : DbContext
    {
        public DbSet<usermodel> registerCMS { get; set; }

        public static string Connectionstring{
            get;
            set;
        }
        public void BuildConnectionstring(string dbstring) { Connectionstring = dbstring; }

        public CustomDbContext(DbContextOptions<CustomDbContext> options) : base(options) { 
        
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<usermodel>(eb =>
            {
                eb.HasKey("username");
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!string.IsNullOrEmpty(Connectionstring))
            {
                optionsBuilder.UseSqlServer(Connectionstring);
            }
        }
    }
}
