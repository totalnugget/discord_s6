using System;
using GuildService.Data;
using GuildService.Data.utils;
using GuildService.Logic.Implementations;
using GuildService.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GuildService.Logic
{
    public static class ServiceMapper
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(ConnectionStringUtil.GetConnectionString())
            );
            
            services.AddTransient<IGuildLogic, GuildLogic>();
            services.AddTransient<IChannelLogic, ChannelLogic>();
            services.AddTransient<IUserLogic, UserLogic>();
        }
    }
}