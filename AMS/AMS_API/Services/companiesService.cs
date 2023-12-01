using AMS_API.Contexts;
using AMS_API.Models;
using Microsoft.EntityFrameworkCore;
using AMS_API.Contexts.Tables;
using AMS_API.Contexts.Views;
using Microsoft.EntityFrameworkCore.Storage;

namespace AMS_API.Services
{
    public class companiesService
    {
        private readonly IConfiguration _configuration;
        private readonly locationsServices _locationService;

        public companiesService(IConfiguration configuration)
        {
            _configuration = configuration;
            _locationService = new locationsServices();
        }
        public async Task<List<vw_companies>> getData(string search)
        {
            using (var context = new AMSDbContext(_configuration))
            {
                if (string.IsNullOrEmpty(search))
                {
                    return await context.vw_companies.ToListAsync();
                }
                else
                {
                    search = search.ToLower();
                    return await context.vw_companies.Where(f => f.company_name.ToLower().Contains(search)).ToListAsync();
                }
            }
        }
        public async Task<returnService> newData(companies req)
        {
            using (var context = new AMSDbContext(_configuration))
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        if (await context.tbl_companies.Where(f => f.company_name.ToLower().Equals(req.company_name.ToLower())).FirstOrDefaultAsync() != null)
                        {
                            return new returnService { status = false, message = "company is already registered" };
                        }
                        var data = new tbl_companies
                        {
                            company_name = req.company_name,
                            phone = req.phone,
                            email = req.email,
                            contact = req.contact,
                            url = req.url,
                            created_at = DateTime.UtcNow,
                            created_by = req.id_user
                        };
                        await context.tbl_companies.AddAsync(data);
                        await context.SaveChangesAsync();

                        await transaction.CommitAsync();
                        return new returnService { status = false, message = "company created successfully!" };
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
        public async Task<returnService> updateData(companies req)
        {
            using (var context = new AMSDbContext(_configuration))
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var data = await context.tbl_companies.Where(f => f.id_company == req.id_company).FirstOrDefaultAsync();
                        if (data == null)
                        {
                            return new returnService { status = false, message = "company is not found" };
                        }
                        data.company_name = req.company_name;
                        data.phone = req.phone;
                        data.email = req.email;
                        data.contact = req.contact;
                        data.url = req.url;
                        data.updated_at = DateTime.UtcNow;
                        data.updated_by = req.id_user;
                        await context.SaveChangesAsync();

                        await transaction.CommitAsync();
                        return new returnService { status = false, message = "company created successfully!" };
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
        public async Task<returnService> updateAddress(company_location req, int id_user)
        {
            using (var context = new AMSDbContext(_configuration))
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {

                        int? id_location = await _locationService.createLocation(context, transaction, req.location, id_user);

                        var data = await context.tbl_companies.Where(f => f.id_company == req.id_company).FirstOrDefaultAsync();
                        if (data != null && id_location != null)
                        {
                            var old_location = await context.tbl_locations.Where(f => f.id_location == data.id_location).FirstOrDefaultAsync();
                            if (old_location != null)
                            {
                                context.tbl_locations.Remove(old_location);
                            }

                            data.id_location = id_location;
                            data.updated_at = DateTime.Now;
                            data.updated_by = id_user;
                            await context.SaveChangesAsync();
                        }

                        await transaction.CommitAsync();

                        return new returnService { status = true, message = "Company address updated successfully!" };
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
        public async Task<returnService> deleteData(companies req)
        {
            using (var context = new AMSDbContext(_configuration))
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var data = await context.tbl_companies.Where(f => f.id_company == req.id_company).FirstOrDefaultAsync();
                        if (data == null)
                        {
                            return new returnService { status = false, message = "company is not found" };
                        }
                        data.deleted = true;
                        data.deleted_at = DateTime.UtcNow;
                        data.deleted_by = req.id_user;
                        await context.SaveChangesAsync();

                        await transaction.CommitAsync();
                        return new returnService { status = false, message = "deleted!" };
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
        public async Task<returnService> removeData(int? id_company)
        {
            using (var context = new AMSDbContext(_configuration))
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var data = await context.tbl_companies.Where(f => f.id_company == id_company).FirstOrDefaultAsync();
                        if (data == null)
                        {
                            return new returnService { status = false, message = "company is not found" };
                        }
                        context.tbl_companies.Remove(data);
                        await context.SaveChangesAsync();

                        await transaction.CommitAsync();
                        return new returnService { status = false, message = "removed!" };
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
        public async Task<int?> AddCompanyNameOnly(AMSDbContext context, IDbContextTransaction? transaction, string name, int id_user)
        {
            try
            {
                var locations = new tbl_companies
                {
                    company_name = name,
                    created_at = DateTime.Now,
                    created_by = id_user
                };
                await context.tbl_companies.AddAsync(locations);
                await context.SaveChangesAsync();
                await transaction.CommitAsync();
                return locations.id_company;
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
