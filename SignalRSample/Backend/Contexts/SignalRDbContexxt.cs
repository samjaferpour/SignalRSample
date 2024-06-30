using Backend.Entities;
using Backend.EntityConfigs;
using Microsoft.EntityFrameworkCore;

namespace Backend.Contexts
{
    public class SignalRDbContexxt : DbContext
    {
        public SignalRDbContexxt(DbContextOptions<SignalRDbContexxt> options) : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies()
                          .UseSqlServer();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryConfigs());
            modelBuilder.ApplyConfiguration(new ProductConfigs());
        }
    }
}
