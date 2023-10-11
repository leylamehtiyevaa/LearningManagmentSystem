using lms.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Configuration;
using System;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<LmsDBContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("lmsDB")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false).AddRoles<IdentityRole>().AddEntityFrameworkStores<LmsDBContext>();

builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

    app.Use(async (context, next) =>
    {
        context.Response.Headers.Add("X-Frame-Options", "ALLOW-FROM https://youtube.com"); // Replace with the allowed domain
        await next();
    });
}





app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.MapRazorPages();
app.Run();



public static class AutoRoleCreate
{
    public static async Task CreateRoles(UserManager<IdentityUser> UserManager, RoleManager<IdentityRole> RoleManager)
    {
        try
        {
            string[] roleNames = { "Admin", "User", "Instructor" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExists = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExists)
                {
                    var roleExist = await RoleManager.RoleExistsAsync(roleName);
                    if (!roleExist)
                    {
                        IdentityRole appuserRole = new IdentityRole();
                        appuserRole.Name = roleName;
                        roleResult = await RoleManager.CreateAsync(appuserRole);
                    }
                }

                var poweruser = new IdentityUser
                {

                    Email = "admin@admin.com",
                    UserName = "admin@admin.com",
                    EmailConfirmed = true,
                    PhoneNumber = "298470"
                };

                string userPWD = "admin321.A"; //Configuration["AppSettings:UserPassword"];
                var _user = await UserManager.FindByEmailAsync(poweruser.Email);
                if (_user == null)
                {
                    var createPowerUser = await UserManager.CreateAsync(poweruser, userPWD);
                    if (createPowerUser.Succeeded)
                    {
                        await UserManager.AddToRoleAsync(poweruser, "Admin");
                    }
                }
            }
        }
        catch (Exception ex) { }
    }

}

