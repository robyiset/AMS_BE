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
        public DbSet<tbl_assets> tbl_assets { get; set; }
        public DbSet<tbl_asset_types> tbl_asset_types { get; set; }
        public DbSet<tbl_asset_waranties> tbl_asset_waranties { get; set; }
        public DbSet<tbl_requested_assets> tbl_requested_assets { get; set; }
        public DbSet<tbl_consumable_assets> tbl_consumable_assets { get; set; }
        public DbSet<tbl_asset_maintenances> tbl_asset_maintenances { get; set; }
        public DbSet<tbl_asset_logs> tbl_asset_logs { get; set; }
        public DbSet<tbl_licences> tbl_licences { get; set; }
        public DbSet<tbl_suppliers> tbl_suppliers { get; set; }
    }
}
