using Microsoft.AspNetCore.Identity;

namespace CoreHRM.Data
{
    public class DbInitializer
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // -- 建立角色 --
            string[] roleNames = { "Admin", "User" };
            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // -- 建立預設管理員 --
            var adminUser = await userManager.FindByEmailAsync("admin@corehrm.com");
            if (adminUser == null)
            {
                var newAdminUser = new ApplicationUser
                {
                    UserName = "admin@corehrm.com",
                    Email = "admin@corehrm.com",
                    EmailConfirmed = true,
                    IsActive = true
                };
                var result = await userManager.CreateAsync(newAdminUser, "Admin@123"); // 請務必使用更安全的密碼！
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newAdminUser, "Admin");
                }
            }
        }
    }
}
