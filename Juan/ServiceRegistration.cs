using Juan.Data;
using Juan.Models;
using Juan.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Juan
{
    public static class ServiceRegistration
    {



        public static void Register(this IServiceCollection services, IConfiguration config) 
        {
            services.AddControllersWithViews();
            services.AddDbContext<JuanDbContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped<LayoutService>();
            services.AddSession(Options =>
            {
                Options.IdleTimeout = TimeSpan.FromMinutes(10);
            });
            services.AddHttpContextAccessor();
            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 7;
                options.User.RequireUniqueEmail = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 3;
            }).AddEntityFrameworkStores<JuanDbContext>().AddDefaultTokenProviders();
        }

    }




    
}
