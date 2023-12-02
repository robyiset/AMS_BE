using AMS_API.Contexts.Tables;
using AMS_API.Contexts;
using AMS_API.Models;
using Microsoft.EntityFrameworkCore;
using AMS_API.Contexts.Views;

namespace AMS_API.Services
{
    public class requestableAssetService
    {
        private readonly IConfiguration _configuration;
        private companiesService _companiesService;

        public requestableAssetService(IConfiguration configuration)
        {
            _configuration = configuration;
            _companiesService = new companiesService(configuration);
        }
        public async Task<List<vw_request_assets>> getData(string search)
        {
            using (var context = new AMSDbContext(_configuration))
            {
                if (string.IsNullOrEmpty(search))
                {
                    return await context.vw_request_assets.ToListAsync();
                }
                else
                {
                    search = search.ToLower();
                    return await context.vw_request_assets.Where(f => f.asset_name.ToLower().Contains(search) || 
                    f.requested_from_company.ToLower().Contains(search) || 
                    f.requested_from_user.ToLower().Contains(search)).ToListAsync();
                }
            }
        }

        public async Task<returnService> newData(add_request_asset req, int id_user)
        {
            using (var context = new AMSDbContext(_configuration))
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var check = await context.tbl_assets.Where(f => f.id_asset == req.id_asset).FirstOrDefaultAsync();
                        if (check == null)
                        {
                            return new returnService { status = false, message = "asset is not found" };
                        }
                        var data = new tbl_requested_assets
                        {
                            id_asset = req.id_asset,
                            id_user = req.id_user,
                            requested_at = req.requested_at,
                            notes = req.notes,
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
                        await context.tbl_requested_assets.AddAsync(data);
                        await context.SaveChangesAsync();

                        await transaction.CommitAsync();
                        return new returnService { status = true, message = "company created successfully!" };
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

        public async Task<returnService> updateData(update_request_asset req, int id_user)
        {
            using (var context = new AMSDbContext(_configuration))
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var data = await context.tbl_requested_assets.Where(f => f.id_request == req.id_request).FirstOrDefaultAsync();
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
                        data.requested_at = req.requested_at;
                        data.notes = req.notes;
                        data.updated_at = DateTime.UtcNow;
                        data.updated_by = id_user;

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

                        await transaction.CommitAsync();
                        return new returnService { status = true, message = "request updated successfully!" };
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

        public async Task<returnService> acceptRequest(accept_request_asset req, int id_user)
        {
            using (var context = new AMSDbContext(_configuration))
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var data = await context.tbl_requested_assets.Where(f => f.id_request == req.id_request).FirstOrDefaultAsync();
                        if (data == null)
                        {
                            return new returnService { status = false, message = "data is not found" };
                        }
                        if (!req.accept)
                        {
                            data.denied_at = DateTime.UtcNow;
                        }
                        data.updated_at = DateTime.UtcNow;
                        data.updated_by = id_user;
                        await context.SaveChangesAsync();

                        await transaction.CommitAsync();
                        return new returnService { status = true, message = "request updated successfully!" };
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

        public async Task<returnService> deleteData(int id_request, int id_user)
        {
            using (var context = new AMSDbContext(_configuration))
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var data = await context.tbl_requested_assets.Where(f => f.id_request == id_request).FirstOrDefaultAsync();
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
        public async Task<returnService> removeData(int? id_request)
        {
            using (var context = new AMSDbContext(_configuration))
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var data = await context.tbl_requested_assets.Where(f => f.id_request == id_request).FirstOrDefaultAsync();
                        if (data == null)
                        {
                            return new returnService { status = false, message = "data is not found" };
                        }
                        context.tbl_requested_assets.Remove(data);
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
