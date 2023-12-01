﻿using AMS_API.Contexts;
using AMS_API.Contexts.Tables;
using AMS_API.Models;
using Microsoft.EntityFrameworkCore;

namespace AMS_API.Services
{
    public class suppliersService
    {
        private readonly IConfiguration _configuration;
        private readonly locationsServices _locationService;
        private companiesService _companiesService;

        public suppliersService(IConfiguration configuration)
        {
            _configuration = configuration;
            _locationService = new locationsServices();
            _companiesService = new companiesService(configuration);
        }
        public async Task<List<suppliers>> getData(string search)
        {
            using (var context = new AMSDbContext(_configuration))
            {
                if (string.IsNullOrEmpty(search))
                {
                    return await context.tbl_suppliers.Select(f => new suppliers
                    {
                        id_supplier = f.id_supplier,
                        supplier_name = f.supplier_name,
                        phone = f.phone,
                        email = f.email,
                        contact = f.contact,
                        url = f.url,
                    }).ToListAsync();
                }
                else
                {
                    search = search.ToLower();
                    return await context.tbl_suppliers.Where(f => f.deleted == false && f.supplier_name.ToLower().Contains(search)).Select(f => new suppliers
                    {
                        id_supplier = f.id_supplier,
                        supplier_name = f.supplier_name,
                        phone = f.phone,
                        email = f.email,
                        contact = f.contact,
                        url = f.url,
                    }).ToListAsync();
                }
                
            }
        }
        public async Task<returnService> newData(suppliers req)
        {
            using (var context = new AMSDbContext(_configuration))
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        //if (await context.tbl_suppliers.Where(f => f.company_name.ToLower().Equals(req.company_name.ToLower())).FirstOrDefaultAsync() != null)
                        //{
                        //    return new returnService { status = false, message = "company is already registered" };
                        //}
                        var data = new tbl_suppliers
                        {
                            supplier_name = req.supplier_name,
                            phone = req.phone,
                            email = req.email,
                            contact = req.contact,
                            url = req.url,
                            created_at = DateTime.UtcNow,
                            created_by = req.id_user
                        };
                        await context.tbl_suppliers.AddAsync(data);
                        await context.SaveChangesAsync();

                        await transaction.CommitAsync();
                        return new returnService { status = false, message = "supplier created successfully!" };
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
        public async Task<returnService> updateData(suppliers req)
        {
            using (var context = new AMSDbContext(_configuration))
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var data = await context.tbl_suppliers.Where(f => f.id_supplier == req.id_supplier).FirstOrDefaultAsync();
                        if (data == null)
                        {
                            return new returnService { status = false, message = "supplier is not found" };
                        }
                        data.supplier_name = req.supplier_name;
                        data.phone = req.phone;
                        data.email = req.email;
                        data.contact = req.contact;
                        data.url = req.url;
                        data.updated_at = DateTime.UtcNow;
                        data.updated_by = req.id_user;
                        await context.SaveChangesAsync();

                        await transaction.CommitAsync();
                        return new returnService { status = false, message = "supplier created successfully!" };
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
        public async Task<returnService> updateAddress(supplier_location req, int id_user)
        {
            using (var context = new AMSDbContext(_configuration))
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        int? id_location = await _locationService.createLocation(context, transaction,
                            new locations
                            {
                                address = req.location.address,
                                city = req.location.city,
                                state = req.location.state,
                                country = req.location.country,
                                zip = req.location.zip,
                                id_supplier = req.id_supplier,
                            },
                            id_user);

                        if (id_location == null || id_location == 0)
                        {
                            return new returnService { status = true, message = "Failed to update supplier address!" };
                        }

                        return new returnService { status = true, message = "supplier address updated successfully!" };
                    }
                    catch (Exception ex)
                    {
                        //throw; 
                        return new returnService { status = false, message = ex.Message };
                    }
                }
            }
        }
        public async Task<returnService> updateCompany(supplier_company req, int id_user)
        {
            using (var context = new AMSDbContext(_configuration))
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        if ((req.company.id_company > 0 && !string.IsNullOrEmpty(req.company.company_name)) ||
                            (req.company.id_company == 0 && string.IsNullOrEmpty(req.company.company_name)))
                        {
                            return new returnService { status = false, message = "Cannot update supplier company!" };
                        }
                        var data = await context.tbl_suppliers.Where(f => f.id_supplier == req.id_supplier).FirstOrDefaultAsync();
                        if (data != null)
                        {
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
                            data.updated_at = DateTime.Now;
                            data.updated_by = id_user;
                            await context.SaveChangesAsync();
                        }

                        await transaction.CommitAsync();

                        return new returnService { status = true, message = "User address updated successfully!" };
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
        public async Task<returnService> deleteData(suppliers req)
        {
            using (var context = new AMSDbContext(_configuration))
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var data = await context.tbl_suppliers.Where(f => f.id_supplier == req.id_supplier).FirstOrDefaultAsync();
                        if (data == null)
                        {
                            return new returnService { status = false, message = "supplier is not found" };
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
        public async Task<returnService> removeData(int? id_supplier)
        {
            using (var context = new AMSDbContext(_configuration))
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var data = await context.tbl_suppliers.Where(f => f.id_supplier == id_supplier).FirstOrDefaultAsync();
                        if (data == null)
                        {
                            return new returnService { status = false, message = "supplier is not found" };
                        }
                        context.tbl_suppliers.Remove(data);
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
