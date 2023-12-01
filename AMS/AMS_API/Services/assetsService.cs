using AMS_API.Contexts.Tables;
using AMS_API.Contexts;
using AMS_API.Models;
using Microsoft.EntityFrameworkCore;

namespace AMS_API.Services
{
    public class assetsService
    {
        private readonly IConfiguration _configuration;
        private readonly locationsServices _locationService;

        public assetsService(IConfiguration configuration)
        {
            _configuration = configuration;
            _locationService = new locationsServices();
        }

        public async Task<returnService> newData(assets req)
        {
            using (var context = new AMSDbContext(_configuration))
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var data = new tbl_assets
                        {
                            serial = req.serial,
                            asset_name = req.asset_name,
                            asset_desc = req.asset_desc,
                            id_type = req.id_type,
                            id_user = req.id_user,
                            purchase_date = req.purchase_date,
                            purchase_cost = req.purchase_cost,
                            depreciate = req.depreciate,
                            requestable = req.requestable,
                            consumable = req.consumable,
                            status = req.status,
                            created_at = DateTime.UtcNow,
                            created_by = req.id_user
                        };
                        await context.tbl_assets.AddAsync(data);
                        await context.SaveChangesAsync();

                        await transaction.CommitAsync();
                        return new returnService { status = false, message = "Asset created successfully!" };
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        //throw; 
                        return new returnService { status = false, message = ex.Message };
                    }
                }
            }
        }
    }
}
