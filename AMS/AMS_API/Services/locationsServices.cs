using AMS_API.Contexts;
using AMS_API.Models;
using Microsoft.EntityFrameworkCore.Storage;

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
                    created_at = DateTime.Now,
                    created_by = id_user
                };
                await context.tbl_locations.AddAsync(locations);
                await context.SaveChangesAsync();
                await transaction.CommitAsync();
                return locations.id_location;
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
