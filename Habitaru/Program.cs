using Habitaru.BLL;
using Habitaru.BLL.Context;
using Habitaru.Models;
using Habitaru.Repositories;
using Habitaru.Repositories.IRepositories;
using Habitaru.Services;
using Habitaru.Services.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Habitaru
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<HabitContext>(options =>
            {
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            });
            builder.Services.AddScoped<IHabitRepository, HabitRepository>();
            builder.Services.AddScoped<IHabitService, HabitService>();
            builder.Services.AddScoped<IAccountRepository, AccountRepository>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>
                (options =>
                {
                    //options.Password.RequireDigit = true;
                    //options.Password.RequireUppercase = true;
                    //options.Password.RequiredUniqueChars = 1;
                })
                .AddEntityFrameworkStores<HabitContext>();
            //for getting user information from classes other than controller
            builder.Services.AddHttpContextAccessor();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Habit}/{action=Index}/{id?}");

            //using (var scope = app.Services.CreateScope())
            //{
            //    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            //    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            //    await InitializeRolesAndUsers(roleManager, userManager);
            //}

            app.Run();
        }
        //private static async Task InitializeRoles(RoleManager<ApplicationRole> roleManager)
        //{
        //    string[] roleNames = { "Admin" };

        //    foreach (var roleName in roleNames)
        //    {
        //        var roleExist = await roleManager.RoleExistsAsync(roleName);
        //        if (!roleExist)
        //        {
        //            // Create the role if it doesn't exist
        //            var role = new ApplicationRole { Name = roleName };
        //            await roleManager.CreateAsync(role);
        //        }
        //    }
        //}
        //private static async Task InitializeRolesAndUsers(
        //RoleManager<ApplicationRole> roleManager,
        //UserManager<ApplicationUser> userManager)
        //{
        //    // Initialize roles
        //    await InitializeRoles(roleManager);

        //    // Create an admin user
        //    var adminEmail = "admin@example.com";
        //    var adminUser = await userManager.FindByEmailAsync(adminEmail);

        //    if (adminUser == null)
        //    {
        //        adminUser = new ApplicationUser
        //        {
        //            UserName = "admin",
        //            Email = adminEmail,
        //        };
        //        await userManager.CreateAsync(adminUser, "YourAdminPasswordHere");
        //    }

        //    // Assign the admin user to the "Admin" role
        //    await userManager.AddToRoleAsync(adminUser, "Admin");
        //}
    }
}
