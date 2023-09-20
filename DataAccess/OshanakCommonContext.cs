using DataModel.Model;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class OshanakCommonContext:DbContext
    {
        DbContextOptions<OshanakCommonContext> _options;
        public OshanakCommonContext(DbContextOptions<OshanakCommonContext> options) : base(options)
        {
            _options = options;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            optionBuilder.UseSqlServer("Data Source=address;Initial Catalog=db;Persist Security Info=True;User ID=DbAdmin;Password=pass;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }

        public virtual DbSet<Web_UserModel> Web_Users { get; set; }
        public virtual DbSet<Web_MenuItem> Web_MenuItems { get; set; }
        public virtual DbSet<Web_Operation> Web_Operations { get; set; }
        public virtual DbSet<Web_WorkgroupOperation> Web_WorkgroupOperations { get; set; }
        public virtual DbSet<Web_WorkgroupUser> Web_WorkgroupUsers { get; set; }
    }
}
