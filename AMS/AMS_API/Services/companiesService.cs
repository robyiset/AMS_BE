using AMS_API.Contexts;
using AMS_API.Models;
using Microsoft.EntityFrameworkCore;

namespace AMS_API.Services
{
    public class companiesService
    {
        private readonly IConfiguration _configuration;

        public companiesService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<List<companies>> getData()
        {
            using (var context = new AMSDbContext(_configuration))
            {
                return await context.tbl_companies.Where(f => f.deleted == false).Select(f => new companies 
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
        public async Task<returnService> createNew(companies req)
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
    }
}
