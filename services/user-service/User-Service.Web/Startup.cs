using System;
using UserService.Data;
using UserService.Logic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using FitKidRabbitMQClient.Extensions;
using UserService.Web.Messaging;

namespace UserService.Web
{
    public class Startup
    {
        private IConfiguration _configuration { get; }
        public Startup(IConfiguration configuration) {
            _configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            //services.AddAuthentication(options => {
            //    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            //})
            //.AddCookie()
            //.AddOpenIdConnect(o =>
            //{
            //    o.ClientId = "";
            //    o.ClientSecret = "";
            //    o.Authority = "https://.onelogin.com/oidc";
            //    o.ResponseType = "code";
            //    o.GetClaimsFromUserInfoEndpoint = true;
            //}
            //);

            // rabitmq
            string password = Environment.GetEnvironmentVariable("RABBITMQ-PASSWORD");
            Uri uri = new Uri($"amqp://user:{password}@rabbitmq:5672");
            string exchange = "main";
            string queueName = "user_service";

            // The progress-Service should listen to the following message
            services.AddMessagePublishing(uri, exchange, queueName, builder => {
                builder.WithHandler<UserDeletedHandler>("UserDeleted");
            });

            services.AddSwaggerGen();

            ServiceMapper.ConfigureServices(services, _configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext dataContext)
        {
            foreach (var migration in dataContext.Database.GetPendingMigrations())
            {
                Console.WriteLine("Running Migrations: " + migration);
            }
            dataContext.Database.Migrate();
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            string path = Environment.GetEnvironmentVariable("Route");

            if(path == null)
            {
                path = "/api/users";
            }
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "docs/{documentName}/swagger.json";
                c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                {
                    swaggerDoc.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"{httpReq.Scheme}://{httpReq.Host.Value}{path}", Description = "this service" } };
                });
            });
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("v1/swagger.json", "user Service");
                s.RoutePrefix = "docs";
            });

            app.UseRouting();

            app.UseCors(options =>
            {
                options.AllowAnyOrigin()
                    .WithMethods("GET", "POST", "PUT", "DELETE")
                    .AllowAnyHeader()
                    .WithExposedHeaders("Content-Disposition");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}