using AMS_API.Contexts.Tables;
using AMS_API.Contexts.Views;
using AMS_API.Contexts;
using AMS_API.Models;
using Microsoft.EntityFrameworkCore;

namespace AMS_API.Services
{
    public class licenseService
    {
        private readonly IConfiguration _configuration;

        public licenseService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<List<vw_licenses>> getData(string search)
        {
            using (var context = new AMSDbContext(_configuration))
            {
                if (string.IsNullOrEmpty(search))
                {
                    return await context.vw_licenses.ToListAsync();
                }
                else
                {
                    search = search.ToLower();
                    return await context.vw_licenses.Where(f => f.license_name.ToLower().Contains(search) ||
                    f.license_version.ToLower().Contains(search) ||
                    f.license_account.ToLower().Contains(search) ||
                    f.license_to_user.ToLower().Contains(search) ||
                    f.asset_name.ToLower().Contains(search)).ToListAsync();
                }
            }
        }

        public async Task<returnService> newData(add_license req, int id_user)
        {
            using (var context = new AMSDbContext(_configuration))
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        
                        var data = new tbl_licences
                        {
                            license_name = req.license_name,
                            license_version = req.license_version,
                            license_account = req.license_account,
                            license_desc = req.license_desc,
                            expired_date = req.expired_date,
                            termination_date = req.termination_date,
                            id_user = req.id_user,
                            purchase_date = req.purchase_date,
                            purchase_cost = req.purchase_cost,
                            created_at = DateTime.UtcNow,
                            created_by = id_user
                        };
                        var check = await context.tbl_assets.Where(f => f.id_asset == req.id_asset).FirstOrDefaultAsync();
                        if (check != null)
                        {
                            data.id_asset = req.id_asset;
                        }
                        await context.tbl_licences.AddAsync(data);
                        await context.SaveChangesAsync();

                        await transaction.CommitAsync();
                        return new returnService { status = true, message = "data consume asset created successfully!" };
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

        public async Task<returnService> updateData(update_license req, int id_user)
        {
            using (var context = new AMSDbContext(_configuration))
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var data = await context.tbl_licences.Where(f => f.id_license == req.id_license).FirstOrDefaultAsync();
                        if (data == null)
                        {
                            return new returnService { status = false, message = "data is not found" };
                        }

                        var check = await context.tbl_assets.Where(f => f.id_asset == req.id_asset).FirstOrDefaultAsync();
                        if (check == null)
                        {
                            return new returnService { status = false, message = "asset is not found" };
                        }
                        data.id_asset = req.id_asset;
                        data.id_user = req.id_user;
                        data.license_name = req.license_name;
                        data.license_version = req.license_version;
                        data.license_desc = req.license_desc;
                        data.license_account = req.license_account;
                        data.purchase_date = req.purchase_date;
                        data.purchase_cost = req.purchase_cost;
                        data.expired_date = req.expired_date;
                        data.termination_date = req.termination_date;
                        data.updated_at = DateTime.UtcNow;
                        data.updated_by = id_user;

                        var check_asset = await context.tbl_assets.Where(f => f.id_asset == req.id_asset).FirstOrDefaultAsync();
                        if (check_asset != null)
                        {
                            data.id_asset = req.id_asset;
                        }

                        await context.SaveChangesAsync();

                        await transaction.CommitAsync();
                        return new returnService { status = true, message = "data consume asset updated successfully!" };
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

        public async Task<returnService> deleteData(int id_license, int id_user)
        {
            using (var context = new AMSDbContext(_configuration))
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var data = await context.tbl_licences.Where(f => f.id_license == id_license).FirstOrDefaultAsync();
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
        public async Task<returnService> removeData(int? id_license)
        {
            using (var context = new AMSDbContext(_configuration))
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var data = await context.tbl_licences.Where(f => f.id_license == id_license).FirstOrDefaultAsync();
                        if (data == null)
                        {
                            return new returnService { status = false, message = "data is not found" };
                        }
                        context.tbl_licences.Remove(data);
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
