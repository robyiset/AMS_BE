using AMS_API.Contexts.Tables;
using AMS_API.Contexts;
using AMS_API.Models;
using Microsoft.EntityFrameworkCore;
using AMS_API.Contexts.Views;
using System.Data;

namespace AMS_API.Services
{
    public class assetsService
    {
        private readonly IConfiguration _configuration;
        private readonly locationsServices _locationService;
        private companiesService _companiesService;

        public assetsService(IConfiguration configuration)
        {
            _configuration = configuration;
            _locationService = new locationsServices();
            _companiesService = new companiesService(configuration);
        }

        public async Task<List<vw_assets>> getData(string search)
        {
            using (var context = new AMSDbContext(_configuration))
            {
                if (string.IsNullOrEmpty(search))
                {
                    return await context.vw_assets.ToListAsync();
                }
                else
                {
                    search = search.ToLower();
                    return await context.vw_assets.Where(f => f.asset_name.ToLower().Contains(search)).ToListAsync();
                }
            }
        }

        public async Task<returnService> newData(new_asset req, int id_user)
        {
            using (var context = new AMSDbContext(_configuration))
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        if (req.asset == null)
                        {
                            return new returnService { status = false, message = "Failed to create new asset" };
                        }
                        var data = new tbl_assets
                        {
                            serial = req.asset.serial,
                            asset_name = req.asset.asset_name,
                            asset_desc = req.asset.asset_desc,
                            id_type = req.asset.id_type,
                            id_user = req   .asset.id_user,
                            purchase_date = req.asset.purchase_date,
                            purchase_cost = req.asset.purchase_cost,
                            depreciate = req.asset.depreciate,
                            requestable = req.asset.requestable,
                            consumable = req.asset.consumable,
                            warranty_expiration = req.asset.warranty_expiration,
                            status = req.asset.status,
                            created_at = DateTime.UtcNow,
                            created_by = id_user
                        };
                        if ((req.company.id_company > 0 && !string.IsNullOrEmpty(req.company.company_name)) ||
                            (req.company.id_company == 0 && string.IsNullOrEmpty(req.company.company_name)))
                        {
                            await transaction.RollbackAsync();
                            return new returnService { status = false, message = "Cannot update asset company!" };
                        }
                        if (req.company.id_company > 0 && string.IsNullOrEmpty(req.company.company_name))
                        {
                            data.id_company = req.company.id_company;
                        }
                        else
                        {
                            var new_company = await _companiesService.AddCompanyNameOnly(context, transaction, req.company.company_name, id_user);
                            if (new_company != null)
                            {
                                data.id_company = new_company;
                            }
                        }
                        await context.tbl_assets.AddAsync(data);
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
                                id_asset = data.id_asset,
                                details = req.location.details
                            },
                            id_user);

                            if (id_location == null || id_location == 0)
                            {
                                await transaction.RollbackAsync();
                                return new returnService { status = false, message = "Failed to add asset location!" };
                            }
                        }
                        
                        await context.tbl_asset_logs.AddAsync(new tbl_asset_logs
                        {
                            id_asset = data.id_asset,
                            action_desc = "create new asset",
                            log_date = DateTime.UtcNow,
                        });
                        await context.SaveChangesAsync();

                        await transaction.CommitAsync();
                        return new returnService { status = true, message = "Asset created successfully!" };
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
        public async Task<returnService> updateAsset(update_asset req, int id_user)
        {
            using (var context = new AMSDbContext(_configuration))
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var data = await context.tbl_assets.Where(f => f.id_asset == req.id_asset).FirstOrDefaultAsync();
                        if (data == null)
                        {
                            return new returnService { status = false, message = "asset is not found" };
                        }
                        data.asset_name = req.asset_name;
                        data.asset_desc = req.asset_desc;
                        data.id_type = req.id_type;
                        data.id_user = req.id_user;
                        data.purchase_date = req.purchase_date;
                        data.purchase_cost = req.purchase_cost;
                        data.depreciate = req.depreciate;
                        data.requestable = req.requestable;
                        data.consumable = req.consumable;
                        data.warranty_expiration = req.warranty_expiration;
                        data.status = req.status;
                        data.created_at = DateTime.UtcNow;
                        data.created_by = id_user;
                        if ((req.company.id_company > 0 && !string.IsNullOrEmpty(req.company.company_name)) ||
                            (req.company.id_company == 0 && string.IsNullOrEmpty(req.company.company_name)))
                        {
                            await transaction.RollbackAsync();
                            return new returnService { status = false, message = "Cannot update asset company!" };
                        }
                        if (req.company.id_company > 0 && string.IsNullOrEmpty(req.company.company_name))
                        {
                            data.id_company = req.company.id_company;
                        }
                        else
                        {
                            var new_company = await _companiesService.AddCompanyNameOnly(context, transaction, req.company.company_name, id_user);
                            if (new_company != null)
                            {
                                data.id_company = new_company;
                            }
                        }
                        await context.SaveChangesAsync();

                        if (req.location != null)
                        {
                            var loc_check = await context.tbl_locations.Where(f => f.id_asset == data.id_asset).FirstOrDefaultAsync();
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
                                id_asset = data.id_asset,
                                details = req.location.details
                            },
                            id_user);

                            if (id_location == null || id_location == 0)
                            {
                                await transaction.RollbackAsync();
                                return new returnService { status = false, message = "Failed to add asset location!" };
                            }
                        }

                        await context.tbl_asset_logs.AddAsync(new tbl_asset_logs
                        {
                            id_asset = data.id_asset,
                            action_desc = "update asset",
                            log_date = DateTime.UtcNow,
                        });
                        await context.SaveChangesAsync();

                        await transaction.CommitAsync();
                        return new returnService { status = true, message = "Asset updated successfully!" };
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
        public async Task<returnService> update_warranty(update_asset_warranty req, int id_user)
        {
            using (var context = new AMSDbContext(_configuration))
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var data = await context.tbl_assets.Where(f => f.id_asset == req.id_asset).FirstOrDefaultAsync();
                        if (data == null)
                        {
                            await transaction.RollbackAsync();
                            return new returnService { status = false, message = "asset is not found" };
                        }
                        data.warranty_expiration = req.warranty_expiration;
                        data.updated_at = DateTime.UtcNow;
                        data.updated_by = id_user;
                        await context.SaveChangesAsync();


                        await context.tbl_asset_logs.AddAsync(new tbl_asset_logs
                        {
                            id_asset = req.id_asset,
                            action_desc = "update asset warranty",
                            log_date = DateTime.UtcNow,
                        });
                        await context.SaveChangesAsync();

                        await transaction.CommitAsync();
                        return new returnService { status = true, message = "asset warranty updated successfully!" };
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
        public async Task<returnService> requestable(activation_asset req, int id_user)
        {
            using (var context = new AMSDbContext(_configuration))
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var data = await context.tbl_assets.Where(f => f.id_asset == req.id_asset).FirstOrDefaultAsync();
                        if (data == null)
                        {
                            return new returnService { status = false, message = "asset is not found" };
                        }
                        data.requestable = req.activation;
                        data.updated_at = DateTime.UtcNow;
                        data.updated_by = id_user;
                        await context.SaveChangesAsync();


                        await context.tbl_asset_logs.AddAsync(new tbl_asset_logs
                        {
                            id_asset = req.id_asset,
                            action_desc = "update requestable asset",
                            log_date = DateTime.UtcNow,
                        });
                        await context.SaveChangesAsync();

                        await transaction.CommitAsync();
                        return new returnService { status = true, message = "requestable asset updated successfully!" };
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
        public async Task<returnService> consumable(activation_asset req, int id_user)
        {
            using (var context = new AMSDbContext(_configuration))
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var data = await context.tbl_assets.Where(f => f.id_asset == req.id_asset).FirstOrDefaultAsync();
                        if (data == null)
                        {
                            return new returnService { status = false, message = "asset is not found" };
                        }
                        data.requestable = req.activation;
                        data.updated_at = DateTime.UtcNow;
                        data.updated_by = id_user;
                        await context.SaveChangesAsync();


                        await context.tbl_asset_logs.AddAsync(new tbl_asset_logs
                        {
                            id_asset = req.id_asset,
                            action_desc = "update consumable asset",
                            log_date = DateTime.UtcNow,
                        });
                        await context.SaveChangesAsync();

                        await transaction.CommitAsync();
                        return new returnService { status = true, message = "consumable asset updated successfully!" };
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
                        var data = await context.tbl_assets.Where(f => f.id_asset == id_asset).FirstOrDefaultAsync();
                        if (data == null)
                        {
                            return new returnService { status = false, message = "company is not found" };
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
    }
}
