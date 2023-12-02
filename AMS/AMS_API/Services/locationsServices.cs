using AMS_API.Contexts;
using AMS_API.Contexts.Tables;
using AMS_API.Models;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq;

namespace AMS_API.Services
{
    public class locationsServices
    {
        public async Task<int?> createLocation(AMSDbContext context, IDbContextTransaction? transaction, locations req, int id_user)
        {
            try
            {
                var locations = new tbl_locations
                {
                    address = req.address,
                    city = req.city,
                    state = req.state,
                    country = req.country,
                    zip = req.zip,
                    details = req.details,
                    id_user = req.id_user,
                    id_company = req.id_company,
                    id_supplier = req.id_supplier,
                    id_asset = req.id_asset,
                    id_usage = req.id_usage,
                    created_at = DateTime.Now,
                    created_by = id_user
                };
                await context.tbl_locations.AddAsync(locations);
                await context.SaveChangesAsync();
                return locations.id_location;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw;
                //return null;
            }
        }
        public async Task<bool> createRangeLocations(AMSDbContext context, IDbContextTransaction? transaction, List<locations> req, int id_user)
        {
            try
            {
                var location = new List<tbl_locations>();
                foreach(var item in req)
                {
                    location.Add(new tbl_locations
                    {
                        address = item.address,
                        city = item.city,
                        state = item.state,
                        country = item.country,
                        zip = item.zip,
                        details = item.details,
                        id_user = item.id_user,
                        id_company = item.id_company,
                        id_supplier = item.id_supplier,
                        id_asset = item.id_asset,
                        id_usage = item.id_usage,
                        created_at = DateTime.Now,
                        created_by = id_user
                    });
                }
                await context.tbl_locations.AddRangeAsync(location);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw;
                //return null;
            }
        }

        public async Task<bool?> deleteLocation(AMSDbContext context, IDbContextTransaction? transaction, tbl_locations data)
        {
            try
            {
                context.tbl_locations.Remove(data);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw;
                //return null;
            }
        }
    }
}
