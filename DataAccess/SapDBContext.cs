using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class SapDBContext : DbContext
    {
        DbContextOptions<SapDBContext> _options;
        public SapDBContext(DbContextOptions<SapDBContext> options) : base(options)
        { _options = options; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            optionBuilder.UseSqlServer("Data Source=address;Initial Catalog=db;Persist Security Info=True;User ID=DbAdmin;Password=pass;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
