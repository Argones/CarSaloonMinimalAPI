using Microsoft.EntityFrameworkCore;

namespace CarSaloonMinimalAPI
{
    public class DataContext : DbContext
    {

        public DataContext() : base()
        {

        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=minimalsaloondb;Trusted_Connection=true;TrustServerCertificate=true");
        }
        public DbSet<Car> Cars { get; set; }
    }

}
