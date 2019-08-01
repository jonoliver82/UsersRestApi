using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using UsersRestApi.Extensions;
using UsersRestApi.Factories;
using UsersRestApi.Interfaces;
using UsersRestApi.Models;
using UsersRestApi.Repositories;
using UsersRestApi.Services;
using UsersRestApi.Validaters;

namespace UsersRestApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Add our Entity Framework Core database
            services.AddDbContext<UsersContext>(opt => opt.UseInMemoryDatabase("Users"));

            // Register our services for Dependency Injection
            services.RegisterFactories();
            services.RegisterRepositories();
            services.RegisterServices();
            services.RegisterValidaters();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            // Ensure InMemory database seeding
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<UsersContext>();
                // Ensure the HasData() methods in the context are executed
                context.Database.EnsureCreated();
            }
        }
    }
}
