using AMS_API.Contexts;
using AMS_API.Models;
using Microsoft.EntityFrameworkCore;

namespace AMS_API.Services
{
    public class usersService
    {
        private readonly IConfiguration _configuration;
        private readonly locationsServices _locationService;

        public usersService(IConfiguration configuration, locationsServices locationService)
        {
            _configuration = configuration;
            _locationService = locationService;
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
                        return new returnService { status = false, message = "User created successfully!" };
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
                        }
                        await transaction.CommitAsync();
                        return new returnService { status = false, message = "User profile updated successfully!" };
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
        public async Task<returnService> updateAddress(locations req, int id_user)
        {
            using (var context = new AMSDbContext(_configuration))
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        
                        int? id_location = await _locationService.createLocation(context, transaction, req, id_user);

                        var user_profile = await context.tbl_user_details.Where(f => f.id_user == id_user).FirstOrDefaultAsync();
                        if (user_profile != null && id_location != null)
                        {
                            var old_location = await context.tbl_locations.Where(f => f.id_location == user_profile.id_location).FirstOrDefaultAsync();
                            context.tbl_locations.Remove(old_location);

                            user_profile.id_location = id_location;
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
                //int? id_location = null;
                //var locations = new tbl_locations
                //{
                //    address = req.address,
                //    city = req.city,
                //    state = req.state,
                //    country = req.country,
                //    zip = req.zip,
                //    created_at = DateTime.Now,
                //    created_by = req.id_user
                //};
                //await context.tbl_locations.AddAsync(locations);
                //await context.SaveChangesAsync();
                //id_location = locations.id_location;
            }
        }
    }
}
