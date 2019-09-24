using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Oil.Bll.Infrastructure;
using Oil.Bll.Interfaces.Infrastructure;
using Oil.Dal;
using Oil.Dal.Interfaces.Repositories;
using Oil.Dal.Repositories;
using Serilog;

namespace Oil.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<OilDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            RegisterDI(services);

            services.AddAutoMapper(typeof(Startup));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
            app.UseCors(c =>
                c.AllowAnyOrigin()
                 .AllowAnyHeader()
                 .AllowAnyMethod()
                 .AllowCredentials()
                 .SetIsOriginAllowedToAllowWildcardSubdomains());
            app.UseHttpsRedirection();
            app.UseMvc();
        }

        /// <summary>
        /// Регистрация DI
        /// </summary>
        /// <param name="services"></param>
        private static void RegisterDI (IServiceCollection services)
        {
            services.AddTransient<ICompanyRepository, CompanyRepository>();
            services.AddTransient<IShopRepository, ShopRepository>();
            services.AddTransient<IFieldRepository, FieldRepository>();
            services.AddTransient<IWellRepository, WellRepository>();
            services.AddTransient<IWellTypeRepository, WellTypeRepository>();
            services.AddTransient<IMessageModelBuilder, MessageModelBuilder>();
        }
    }
}
