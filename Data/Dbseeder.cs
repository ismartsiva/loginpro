using loginprac.Areas.Identity.Data;
using loginprac.Constants;
using Microsoft.AspNetCore.Identity;

namespace loginprac.Data
{
    public static class Dbseeder
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider service)
        {
            //Seed Roles
            var userManager = service.GetService<UserManager<loginUser>>();
            var roleManager = service.GetService<RoleManager<IdentityRole>>();
            var Adm = new IdentityRole(Roles.Admin.ToString());
            if (Adm != null)
            {
                await roleManager.CreateAsync(Adm);
            }
            var Adm1 = new IdentityRole(Roles.User.ToString());
            if (Adm1 != null)
            {
            await roleManager.CreateAsync(Adm1);

            }

            // creating admin

            var user = new loginUser
            {
                UserName = "admin1@gmail.com",
                Email = "admin1@gmail.com",
                FirstName = "Ravindra",
                LastName = "tagore",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            var userInDb = await userManager.FindByEmailAsync(user.Email);
            if (userInDb == null)
            {
                await userManager.CreateAsync(user, "Admin@123");
                await userManager.AddToRoleAsync(user, Roles.Admin.ToString());
            }
        }
    }
}
