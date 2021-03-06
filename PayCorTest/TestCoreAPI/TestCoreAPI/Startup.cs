using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using TestCoreAPI.AutoMapper;
using TestCoreAPI.IServices;
using TestCoreAPI.Models;
using TestCoreAPI.Services;

namespace TestCoreAPI
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
            //Inject AppSettings
            services.Configure<ApplicationSettings>(Configuration.GetSection("ApplicationSettings"));

            services.AddControllers();

            //database options
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperDefinition());
            });

            IMapper mapper = mappingConfig.CreateMapper();

            //register dependency injection for services
            services.AddSingleton(mapper);
            services.AddTransient<IPaycorService, PaycorService>();

            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseCors(builder =>
                builder.WithOrigins(Configuration["ApplicationSettings:Client_URL"].ToString())
                        .AllowAnyHeader()
                        .AllowAnyMethod());

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
