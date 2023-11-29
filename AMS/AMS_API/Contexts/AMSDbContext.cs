using Microsoft.EntityFrameworkCore;

namespace AMS_API.Contexts
{
    public class AMSDbContext : DbContext
    {
        protected readonly IConfiguration _configuration;

        public AMSDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            string connectionString = _configuration["ConnectionStrings:SqlServer"]!;

            options.UseSqlServer(connectionString);
        }

        public DbSet<tbl_users> tbl_users { get; set; }
        public DbSet<tbl_user_details> tbl_user_details { get; set; }
        public DbSet<tbl_companies> tbl_companies { get; set; }
        public DbSet<tbl_locations> tbl_locations { get; set; }

    }
}
