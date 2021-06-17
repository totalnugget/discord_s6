using System;
using ChannelService.Data;
using ChannelService.Data.utils;
using ChannelService.Logic.Implementations;
using ChannelService.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChannelService.Logic
{
    public static class ServiceMapper
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(ConnectionStringUtil.GetConnectionString())
            );
            
            services.AddTransient<IChannelLogic, ChannelLogic>();
        }
    }
}