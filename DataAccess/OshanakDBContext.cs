using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class OshanakDBContext : DbContext
    {
        DbContextOptions<OshanakDBContext> _options;
        public OshanakDBContext(DbContextOptions<OshanakDBContext> options) : base(options)
        { _options = options; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            optionBuilder.UseSqlServer("Data Source=address;Initial Catalog=db;Persist Security Info=True;User ID=DbAdmin;Password=pass;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False;")
                .EnableSensitiveDataLogging(true);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
