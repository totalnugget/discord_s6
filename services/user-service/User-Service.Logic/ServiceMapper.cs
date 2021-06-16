using System;
using UserService.Data;
using UserService.Data.utils;
using UserService.Logic.Implementations;
using UserService.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace UserService.Logic
{
    public static class ServiceMapper
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(ConnectionStringUtil.GetConnectionString())
            );
            
            services.AddTransient<IUserLogic, UserLogic>();
        }
    }
}