using AMS_API.Contexts;
using AMS_API.Contexts.Tables;
using AMS_API.Contexts.Views;
using AMS_API.Models;
using Microsoft.EntityFrameworkCore;

namespace AMS_API.Services
{
    public class usersService
    {
        private readonly IConfiguration _configuration;
        private  locationsServices _locationService;
        private companiesService _companiesService;

        public usersService(IConfiguration configuration)
        {
            _configuration = configuration;
            _locationService = new locationsServices();
            _companiesService = new companiesService(configuration);
        }
        public async Task<List<vw_users>> getUsers(string search)
        {
            using (var context = new AMSDbContext(_configuration))
            {
                if (string.IsNullOrEmpty(search))
                {
                    return await context.vw_users.ToListAsync();
                }
                else
                {
                    search = search.ToLower();
                    return await context.vw_users.Where(f => string.IsNullOrEmpty(search) ||
                    f.username.ToLower().Contains(search) || f.email.ToLower().Contains(search) ||
                    (f.first_name + " " + f.last_name).ToLower().Contains(search)).ToListAsync();
                }
            }
        }
        public async Task<List<vw_user_details>> getDetailUser(string search)
        {
            using (var context = new AMSDbContext(_configuration))
            {
                if (string.IsNullOrEmpty(search))
                {
                    return await context.vw_user_details.ToListAsync();
                }
                else
                {
                    string _search = string.IsNullOrEmpty(search) ? "" : search.ToLower();
                    return await context.vw_user_details.Where(f => f.username.ToLower().Contains(_search) ||
                    f.email.ToLower().Contains(_search) ||
                    (f.first_name + " " + f.last_name).ToLower().Contains(_search)).ToListAsync();
                }
            }
        }
        public async Task<returnService> register(register req)
        {
            using (var context = new AMSDbContext(_configuration))
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        if (await context.tbl_users.Where(f => f.email == req.email || f.username == req.username).FirstOrDefaultAsync() != null)
                        {
                            return new returnService { status = false, message = "User is already registered" };
                        }
                        var newUser = new tbl_users
                        {
                            username = req.username,
                            email = req.email,
                            password = BCrypt.Net.BCrypt.HashPassword(req.password),
                            created_at = DateTime.UtcNow,
                        };
                        await context.tbl_users.AddAsync(newUser);
                        await context.SaveChangesAsync();

                        await transaction.CommitAsync();
                        return new returnService { status = true, message = "User created successfully!" };
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
        public async Task<returnService> udpateProfile(profile req)
        {
            using (var context = new AMSDbContext(_configuration))
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var user_profile = await context.tbl_user_details.Where(f => f.id_user == req.id_user).FirstOrDefaultAsync();
                        if (user_profile != null)
                        {
                            user_profile.first_name = req.first_name;
                            user_profile.last_name = req.last_name;
                            user_profile.phone_number = req.phone_number;
                            user_profile.about = req.about;
                            user_profile.updated_at = DateTime.Now;
                            user_profile.updated_by = req.id_user;
                            await context.SaveChangesAsync();
                        }
                        else
                        {
                            await context.tbl_user_details.AddAsync(new tbl_user_details
                            {
                                id_user = req.id_user,
                                first_name = req.first_name,
                                last_name = req.last_name,
                                phone_number = req.phone_number,
                                about = req.about,
                                created_at = DateTime.Now,
                                created_by = req.id_user
                            });
                            await context.SaveChangesAsync();
                        }
                        await transaction.CommitAsync();
                        return new returnService { status = true, message = "User profile updated successfully!" };
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
        public async Task<returnService> updateAddress(user_location req, int id_user)
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
                                id_user = req.id_user,
                                details = req.location.details
                            },
                            id_user);

                        if (id_location == null || id_location == 0)
                        {
                            return new returnService { status = true, message = "Failed to update user address!" };
                        }

                        return new returnService { status = true, message = "User address updated successfully!" };
                    }
                    catch (Exception ex)
                    {
                        //throw; 
                        return new returnService { status = false, message = ex.Message };
                    }
                }
            }
        }
        public async Task<returnService> updateCompany(user_company req, int id_user)
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
                            return new returnService { status = false, message = "Cannot update user company!" };
                        }
                        var user_profile = await context.tbl_user_details.Where(f => f.id_user == req.id_user).FirstOrDefaultAsync();
                        if (user_profile != null)
                        {
                            if (req.company.id_company > 0 && string.IsNullOrEmpty(req.company.company_name))
                            {
                                user_profile.id_company = req.company.id_company;
                            }
                            else
                            {
                                var new_company = await _companiesService.AddCompanyNameOnly(context, transaction, req.company.company_name, id_user);
                                if (new_company != null)
                                {
                                    user_profile.id_company = new_company;
                                }
                            }
                            user_profile.updated_at = DateTime.Now;
                            user_profile.updated_by = id_user;
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
    }
}
