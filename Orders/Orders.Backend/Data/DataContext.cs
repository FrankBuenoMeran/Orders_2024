using Microsoft.EntityFrameworkCore;
using Orders.Shared.Entities;

namespace Orders.Backend.Data
{
    public class DataContext : DbContext
    {
        //Para conectarnos a la base de datos creamos el constructor con la palabra ctor tab
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {  
        }
        public DbSet<Country>Countries{get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().HasIndex(x =>x.Name).IsUnique();
        }
    }
}
