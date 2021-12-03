using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MovieStars.Models
{
    public class RoleInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext db)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            //создаём две роли
            var adminRole = new IdentityRole { Name = "admin" };
            var userRole = new IdentityRole { Name = "user" };

            //добавляем роли в БД
            roleManager.Create(adminRole);
            roleManager.Create(userRole);

            //создаём пользователей
            var admin = new ApplicationUser { Email = "Admin@ya.ru", UserName = "Admin@ya.ru" };
            string password = "Pass123@";
            var result = userManager.Create(admin, password);

            //если создание пользователя прошло успешно
            if (result.Succeeded)
            {
                //добавляем для пользователя роль
                userManager.AddToRole(admin.Id, adminRole.Name);
                userManager.AddToRole(admin.Id, userRole.Name);
            }
            base.Seed(db);
        }
        
}
}