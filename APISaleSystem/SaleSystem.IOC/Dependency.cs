using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SaleSystem.DAL.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SaleSystem.DAL.Repositories.Interfaces;
using SaleSystem.DAL.Repositories;
using SaleSystem.Utility;
using SaleSystem.BLL.Services.Interfaces;
using SaleSystem.BLL.Services;
using SaleSystem.BLL.Services.Interfaces;
namespace SaleSystem.IOC
{
    public static class Dependency
    {
        public static void dependencyInjection(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<DbsalesContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("stringSQL"));
            });

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ISaleRepository, SaleRepository>();
            services.AddAutoMapper(typeof(AutoMapperProfile));


            services.AddScoped<IRolService, RolService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ISaleService, SaleService>();
            services.AddScoped<IDashboardService, DashboardService>();
            services.AddScoped<IMenuService, MenuService>();
        }
    }
}
