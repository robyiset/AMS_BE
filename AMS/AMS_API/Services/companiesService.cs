using AMS_API.Contexts;
using AMS_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using System;
using AMS_API.Contexts.Tables;

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
        public async Task<List<companies>> getData(string search)
        {
            using (var context = new AMSDbContext(_configuration))
            {
                return await context.tbl_companies.Where(f => f.deleted == false && f.company_name.ToLower().Contains(search.ToLower())).Select(f => new companies 
                { 
                    id_company = f.id_company, 
                    company_name = f.company_name,  
                    phone = f.phone,
                    email = f.email,
                    contact = f.contact,
                    url = f.url,
                }).ToListAsync();
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
        public async Task<returnService> updateAddress(locations req, int id_company, int id_user)
        {
            using (var context = new AMSDbContext(_configuration))
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {

                        int? id_location = await _locationService.createLocation(context, transaction, req, id_user);

                        var data = await context.tbl_companies.Where(f => f.id_company == id_company).FirstOrDefaultAsync();
                        if (data != null && id_location != null)
                        {
                            var old_location = await context.tbl_locations.Where(f => f.id_location == data.id_location).FirstOrDefaultAsync();
                            context.tbl_locations.Remove(old_location);

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
    }
}
