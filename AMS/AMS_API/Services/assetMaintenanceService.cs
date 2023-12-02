using AMS_API.Contexts.Tables;
using AMS_API.Contexts;
using AMS_API.Models;
using Microsoft.EntityFrameworkCore;
using AMS_API.Contexts.Views;

namespace AMS_API.Services
{
    public class assetMaintenanceService
    {
        private readonly IConfiguration _configuration;
        private readonly locationsServices _locationService;

        public assetMaintenanceService(IConfiguration configuration)
        {
            _configuration = configuration;
            _locationService = new locationsServices();
        }

        public async Task<List<vw_maintenance>> getData(string search)
        {
            using (var context = new AMSDbContext(_configuration))
            {
                if (string.IsNullOrEmpty(search))
                {
                    return await context.vw_maintenance.ToListAsync();
                }
                else
                {
                    search = search.ToLower();
                    return await context.vw_maintenance.Where(f => f.title.ToLower().Contains(search) || f.asset_name.ToLower().Contains(search)).ToListAsync();
                }
            }
        }

        public async Task<returnService> newData(add_maintenance req, int id_user)
        {
            using (var context = new AMSDbContext(_configuration))
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        if (req == null)
                        {
                            return new returnService { status = false, message = "Failed to create new data" };
                        }
                        var check = await context.tbl_assets.Where(f => f.id_asset == req.maintenance.id_asset).FirstOrDefaultAsync();
                        if (check == null)
                        {
                            return new returnService { status = false, message = "asset is not found" };
                        }
                        var data = new tbl_asset_maintenances
                        {
                            id_asset = req.maintenance.id_asset,
                            title = req.maintenance.title,
                            maintenance_desc = req.maintenance.maintenance_desc,
                            start_date = req.maintenance.start_date,
                            completion_date = req.maintenance.completion_date,
                            maintenance_time = req.maintenance.maintenance_time,
                            cost = req.maintenance.cost,
                            notes = req.maintenance.notes,
                            created_at = DateTime.UtcNow,
                            created_by = id_user
                        };
                        await context.tbl_asset_maintenances.AddAsync(data);
                        await context.SaveChangesAsync();

                        if (req.location != null)
                        {
                            int? id_location = await _locationService.createLocation(context, transaction,
                            new locations
                            {
                                address = req.location.address,
                                city = req.location.city,
                                state = req.location.state,
                                country = req.location.country,
                                zip = req.location.zip,
                                id_maintenance = data.id_maintenance,
                                details = req.location.details
                            },
                            id_user);

                            if (id_location == null || id_location == 0)
                            {
                                await transaction.RollbackAsync();
                                return new returnService { status = false, message = "Failed to add maintenance location!" };
                            }
                        }

                        await context.tbl_asset_logs.AddAsync(new tbl_asset_logs
                        {
                            id_asset = data.id_asset,
                            action_desc = "create new asset maintenance",
                            log_date = DateTime.UtcNow,
                        });
                        await context.SaveChangesAsync();

                        await transaction.CommitAsync();
                        return new returnService { status = true, message = "maintenance created successfully!" };
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
        public async Task<returnService> updateData(update_maintenance req, int id_user)
        {
            using (var context = new AMSDbContext(_configuration))
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var data = await context.tbl_asset_maintenances.Where(f => f.id_maintenance == req.id_maintenance).FirstOrDefaultAsync();
                        if (data == null)
                        {
                            return new returnService { status = false, message = "maintenance is not found" };
                        }
                        var check = await context.tbl_assets.Where(f => f.id_asset == req.id_asset).FirstOrDefaultAsync();
                        if (check == null)
                        {
                            return new returnService { status = false, message = "asset is not found" };
                        }
                        data.id_asset = req.id_asset;
                        data.title = req.title;
                        data.maintenance_desc = req.maintenance_desc;
                        data.start_date = req.start_date;
                        data.completion_date = req.completion_date;
                        data.maintenance_time = req.maintenance_time;
                        data.cost = req.cost;
                        data.notes = req.notes;
                        data.updated_at = DateTime.UtcNow;
                        data.updated_by = id_user;
                        await context.SaveChangesAsync();

                        if (req.location != null)
                        {
                            var loc_check = await context.tbl_locations.Where(f => f.id_maintenance == data.id_maintenance).FirstOrDefaultAsync();
                            if (loc_check != null)
                            {
                                _locationService.deleteLocation(context, transaction, loc_check);
                            }
                            int? id_location = await _locationService.createLocation(context, transaction,
                            new locations
                            {
                                address = req.location.address,
                                city = req.location.city,
                                state = req.location.state,
                                country = req.location.country,
                                zip = req.location.zip,
                                details = req.location.details,
                                id_maintenance = data.id_maintenance,
                            },
                            id_user);

                            if (id_location == null || id_location == 0)
                            {
                                await transaction.RollbackAsync();
                                return new returnService { status = false, message = "Failed to add maintenance location!" };
                            }
                        }

                        await context.tbl_asset_logs.AddAsync(new tbl_asset_logs
                        {
                            id_asset = data.id_asset,
                            action_desc = "update asset maintenance",
                            log_date = DateTime.UtcNow,
                        });
                        await context.SaveChangesAsync();

                        await transaction.CommitAsync();
                        return new returnService { status = true, message = "maintenance updated successfully!" };
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
        public async Task<returnService> deleteData(int id_asset, int id_user)
        {
            using (var context = new AMSDbContext(_configuration))
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var data = await context.tbl_asset_maintenances.Where(f => f.id_asset == id_asset).FirstOrDefaultAsync();
                        if (data == null)
                        {
                            return new returnService { status = false, message = "data is not found" };
                        }
                        data.deleted = true;
                        data.deleted_at = DateTime.UtcNow;
                        data.deleted_by = id_user;
                        await context.SaveChangesAsync();

                        await transaction.CommitAsync();
                        return new returnService { status = true, message = "deleted!" };
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
        public async Task<returnService> removeData(int? id_maintenance)
        {
            using (var context = new AMSDbContext(_configuration))
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var data = await context.tbl_asset_maintenances.Where(f => f.id_maintenance == id_maintenance).FirstOrDefaultAsync();
                        if (data == null)
                        {
                            return new returnService { status = false, message = "maintenance is not found" };
                        }
                        var loc_check = await context.tbl_locations.Where(f => f.id_maintenance == data.id_maintenance).FirstOrDefaultAsync();
                        if (loc_check != null)
                        {
                            _locationService.deleteLocation(context, transaction, loc_check);
                        }
                        context.tbl_asset_maintenances.Remove(data);
                        await context.SaveChangesAsync();

                        await transaction.CommitAsync();
                        return new returnService { status = true, message = "removed!" };
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
