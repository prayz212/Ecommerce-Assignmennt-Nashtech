using BackEnd.Repositories;
using BackEnd.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FluentValidation.AspNetCore;
using BackEnd.Validations;
using System;
using BackEnd.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using BackEnd.Utils;

namespace BackEnd
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
            services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(
                Configuration.GetConnectionString("ApplicationDbConnect")
            ));

            services.AddIdentity<ApplicationUser, IdentityRole>(
                opts => opts.Password = new PasswordOptions
                {
                    RequireDigit = false,
                    RequiredLength = 8,
                    RequireLowercase = false,
                    RequireUppercase = false,
                    RequireNonAlphanumeric = false
                }
            )
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(opts =>
            {
                opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(opts => opts.JwtBearerOptionsConfig(
                Configuration["JWT:ValidAudience"], 
                Configuration["JWT:ValidIssuer"],
                Configuration["JWT:Secret"]
            ));

            services.AddCors();

            services.AddControllers().AddFluentValidation();
            services.AddSwaggerGen(c => c.SwaggerGenOptionsConfig());

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddRepositoriesDependencyInjection();
            services.AddServicesDependencyInjection();
            services.AddValidatesDependencyInjection();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "APIs v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(opts => opts
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true)
                .AllowCredentials());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
