using AvdoshkaMMM.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AvdoshkaMMM.Infrastructure.Data
{
    public class DbInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            UserManager<User> userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            ILogger<DbInitializer> logger = scope.ServiceProvider.GetRequiredService<ILogger<DbInitializer>>();

            string[] roles = {"Admin", "User"};
            foreach(string role in roles)
            {
                if(!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                    logger.LogInformation("Роли успешно созданы");
                }
                else
                {
                    logger.LogInformation("Ошибка создания роли/Роли существуют");
                }
            }

            string aEmail = "Admin@mail.ru";
            string aPassword = "@dminPassword667";
            User? adminUser = await userManager.FindByNameAsync(aEmail);
            if(adminUser == null)
            {
                adminUser = new User
                {
                    UserName = "Admin",
                    Email = aEmail,
                    FirstName = "Admin",
                    LastName = "Admin",
                    Pathronomic = "Admin",
                    EmailConfirmed = true,
                };

                IdentityResult result = await userManager.CreateAsync(adminUser, aPassword);
                if (result.Succeeded)
                {
                    IdentityResult addToRoleAsync = await userManager.AddToRoleAsync(adminUser, "Admin");
                    if (addToRoleAsync.Succeeded)
                    {
                        logger.LogInformation($"Аккаунт {adminUser.UserName} успешно создан с ролью Admin");
                    }
                    else
                    {
                        logger.LogError($"Пользователь существует/ошибка при присвоении роли Admin. Описание : {string.Join(" ", addToRoleAsync.Errors)}");
                    }
                }
                else
                {
                    logger.LogError("Ошибка создания пользователя Admin");
                }
            }
            else
            {
                logger.LogError("Пользователь Admin уже существует");
            }
        }
    }
}