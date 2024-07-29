using Juan.Data;
using Juan.Services;
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

        }




    }
}
