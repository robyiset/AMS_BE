using AMS_API.Contexts.Tables;
using AMS_API.Contexts;
using AMS_API.Models;
using Microsoft.EntityFrameworkCore;

namespace AMS_API.Services
{
    public class assetTypeService
    {
        private readonly IConfiguration _configuration;

        public assetTypeService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<List<asset_type>> getData(string search)
        {
            using (var context = new AMSDbContext(_configuration))
            {
                var data = string.IsNullOrEmpty(search) ?
                    await context.tbl_asset_types.Select(f => new asset_type { name_type = f.name_type, desc_type = f.desc_type }).ToListAsync() :
                    await context.tbl_asset_types.Where(f => f.name_type.ToLower().Contains(search))
                    .Select(f => new asset_type { name_type = f.name_type, desc_type = f.desc_type }).ToListAsync();
                
                return data;
            }
        }

        public async Task<returnService> NewAssetType(add_asset_type req, int id_user)
        {
            using (var context = new AMSDbContext(_configuration))
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {

                        var data = new tbl_asset_types
                        {
                            name_type = req.name_type,
                            desc_type = req.desc_type,
                            created_at = DateTime.UtcNow,
                            created_by = id_user
                        };

                        await context.tbl_asset_types.AddAsync(data);
                        await context.SaveChangesAsync();

                        await transaction.CommitAsync();
                        return new returnService { status = true, message = "Asset type created successfully!" };
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

        public async Task<returnService> AddRangeAssetType(List<add_asset_type> req, int id_user)
        {
            using (var context = new AMSDbContext(_configuration))
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {

                        var data = new List<tbl_asset_types>();
                        foreach (var item in req)
                        {
                            data.Add(new tbl_asset_types
                            {
                                name_type = item.name_type,
                                desc_type = item.desc_type,
                                created_at = DateTime.UtcNow,
                                created_by = id_user
                            });
                        }

                        await context.tbl_asset_types.AddRangeAsync(data);
                        await context.SaveChangesAsync();

                        await transaction.CommitAsync();
                        return new returnService { status = true, message = "Asset types created successfully!" };
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
        public async Task<returnService> updateData(asset_type req)
        {
            using (var context = new AMSDbContext(_configuration))
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var data = await context.tbl_asset_types.Where(f => f.id_type == req.id_type).FirstOrDefaultAsync();
                        if (data == null)
                        {
                            return new returnService { status = false, message = "asset type is not found" };
                        }
                        data.name_type = req.name_type;
                        data.desc_type = req.desc_type;
                        data.created_at = DateTime.UtcNow;
                        data.created_by = req.id_user;
                        await context.SaveChangesAsync();

                        await transaction.CommitAsync();
                        return new returnService { status = true, message = "asset type created successfully!" };
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
        public async Task<returnService> deleteData(asset_type req)
        {
            using (var context = new AMSDbContext(_configuration))
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var data = await context.tbl_asset_types.Where(f => f.id_type == req.id_type).FirstOrDefaultAsync();
                        if (data == null)
                        {
                            return new returnService { status = false, message = "asset type is not found" };
                        }
                        data.deleted = true;
                        data.deleted_at = DateTime.UtcNow;
                        data.deleted_by = req.id_user;
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
        public async Task<returnService> removeData(int? id_type)
        {
            using (var context = new AMSDbContext(_configuration))
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var data = await context.tbl_asset_types.Where(f => f.id_type == id_type).FirstOrDefaultAsync();
                        if (data == null)
                        {
                            return new returnService { status = false, message = "Type is not found" };
                        }
                        context.tbl_asset_types.Remove(data);
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
