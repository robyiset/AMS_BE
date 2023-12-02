using AMS_API.Contexts.Tables;
using AMS_API.Contexts.Views;
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

        #region tables
        public DbSet<tbl_users> tbl_users { get; set; }
        public DbSet<tbl_user_details> tbl_user_details { get; set; }
        public DbSet<tbl_companies> tbl_companies { get; set; }
        public DbSet<tbl_locations> tbl_locations { get; set; }
        public DbSet<tbl_assets> tbl_assets { get; set; }
        public DbSet<tbl_asset_types> tbl_asset_types { get; set; }
        public DbSet<tbl_requested_assets> tbl_requested_assets { get; set; }
        public DbSet<tbl_consumable_assets> tbl_consumable_assets { get; set; }
        public DbSet<tbl_asset_maintenances> tbl_asset_maintenances { get; set; }
        public DbSet<tbl_asset_logs> tbl_asset_logs { get; set; }
        public DbSet<tbl_licences> tbl_licences { get; set; }
        public DbSet<tbl_suppliers> tbl_suppliers { get; set; }
        #endregion
        #region views
        public DbSet<vw_users> vw_users { get; set; }
        public DbSet<vw_user_details> vw_user_details { get; set; }
        public DbSet<vw_companies> vw_companies { get; set; }
        public DbSet<vw_assets> vw_assets { get; set; }
        public DbSet<vw_maintenance> vw_maintenance { get; set; }
        public DbSet<vw_request_assets> vw_request_assets { get; set; }
        public DbSet<vw_consumable_assets> vw_consumable_assets { get; set; }
        public DbSet<vw_licenses> vw_licenses { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tbl_users>().Property(b => b.created_at).HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<tbl_users>().Property(b => b.activated).HasDefaultValue(true);
            modelBuilder.Entity<tbl_users>().Property(b => b.deleted).HasDefaultValue(false);
            modelBuilder.Entity<tbl_user_details>().Property(b => b.created_at).HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<tbl_user_details>().Property(b => b.deleted).HasDefaultValue(false);
            modelBuilder.Entity<tbl_companies>().Property(b => b.created_at).HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<tbl_companies>().Property(b => b.deleted).HasDefaultValue(false);
            modelBuilder.Entity<tbl_locations>().Property(b => b.created_at).HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<tbl_locations>().Property(b => b.deleted).HasDefaultValue(false);
            modelBuilder.Entity<tbl_assets>().Property(b => b.created_at).HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<tbl_assets>().Property(b => b.deleted).HasDefaultValue(false);
            modelBuilder.Entity<tbl_asset_types>().Property(b => b.created_at).HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<tbl_asset_types>().Property(b => b.deleted).HasDefaultValue(false);
            modelBuilder.Entity<tbl_requested_assets>().Property(b => b.created_at).HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<tbl_requested_assets>().Property(b => b.deleted).HasDefaultValue(false);
            modelBuilder.Entity<tbl_consumable_assets>().Property(b => b.created_at).HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<tbl_consumable_assets>().Property(b => b.deleted).HasDefaultValue(false);
            modelBuilder.Entity<tbl_asset_maintenances>().Property(b => b.created_at).HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<tbl_asset_maintenances>().Property(b => b.deleted).HasDefaultValue(false);
            modelBuilder.Entity<tbl_asset_logs>().Property(b => b.log_date).HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<tbl_licences>().Property(b => b.created_at).HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<tbl_licences>().Property(b => b.deleted).HasDefaultValue(false);
            modelBuilder.Entity<tbl_suppliers>().Property(b => b.created_at).HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<tbl_suppliers>().Property(b => b.deleted).HasDefaultValue(false);
            modelBuilder.Entity<tbl_suppliers>().Property(b => b.created_at).HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<tbl_suppliers>().Property(b => b.deleted).HasDefaultValue(false);
        }

    }
}
