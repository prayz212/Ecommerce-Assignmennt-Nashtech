using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BackEnd.Utils
{
    public static class JwtConfig
    {
        public static OpenApiSecurityScheme JwtSecurityScheme = new OpenApiSecurityScheme
        {
            Scheme = "Bearer",
            BearerFormat = "JWT",
            Name = "JWT Authentication",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Description = @"JWT Authorization header using the Bearer scheme. <br/>
                        Enter 'Bearer' [space] and then your token in the text input below.
                        <br/>Example: 'Bearer 12345abcdef'",
            Reference = new OpenApiReference
            {
                Id = JwtBearerDefaults.AuthenticationScheme,
                Type = ReferenceType.SecurityScheme,
            }
        };

        public static SwaggerGenOptions SwaggerGenOptionsConfig(this SwaggerGenOptions option)
        {
            option.SwaggerDoc("v1", new OpenApiInfo { Title = "APIs", Version = "v1" });
            option.AddSecurityDefinition(JwtConfig.JwtSecurityScheme.Reference.Id, JwtConfig.JwtSecurityScheme);
            option.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                { JwtConfig.JwtSecurityScheme, Array.Empty<string>() }
            });

            return option;
        }

        public static JwtBearerOptions JwtBearerOptionsConfig(this JwtBearerOptions opts, string validAudience, string validIssuer, string key)
        {
            opts.SaveToken = true;
            opts.RequireHttpsMetadata = false;
            opts.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = validAudience,
                ValidIssuer = validIssuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
            };

            return opts;
        }
    }
}