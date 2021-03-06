﻿using Infrastructure.OperationFilters;
using Infrastructure.Caching;
using Infrastructure.Configuration;
using Infrastructure.Data;
using Infrastructure.Identity;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;

namespace ModelMob.SPA.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomCache(this IServiceCollection services)
        {
            services.AddMemoryCache();

            services.TryAddSingleton<ICache, MemoryCache>();

            return services;
        }

        public static IServiceCollection AddCustomConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AuthenticationSettings>(configuration.GetSection("Authentication"));
            return services;
        }

        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new Info
                {
                    Title = "Model Mob",
                    Version = "v1",
                    Description = "Model Mob REST API",
                });
                options.CustomSchemaIds(x => x.FullName);
            });

            services.ConfigureSwaggerGen(options =>
            {
                options.OperationFilter<AuthorizationHeaderParameterOperationFilter>();
                options.OperationFilter<TenantHeaderParameterOperationFilter>();
            });

            return services;
        }
        
        public static IServiceCollection AddDataStore(this IServiceCollection services,
                                               string connectionString)
        {
            //Note: this has to come first
            services.AddScoped<IAppDbContext, AppDbContext>();

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionString, b => b.MigrationsAssembly("Infrastructure"));
            });

            return services;
        }

        public static IServiceCollection AddSecurity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options => options.AddPolicy("CorsPolicy",
                builder => builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()));

            services.AddHttpContextAccessor();

            services.TryAddSingleton<ITokenProvider, TokenProvider>();

            services.AddSingleton<IEncryptionService, EncryptionService>();

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler
            {
                InboundClaimTypeMap = new Dictionary<string, string>()
            };

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.SecurityTokenValidators.Clear();
                    options.SecurityTokenValidators.Add(jwtSecurityTokenHandler);
                    options.TokenValidationParameters = GetTokenValidationParameters(configuration);
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            if ((context.Request.Path.Value.StartsWith("/chatHub"))
                                && context.Request.Query.TryGetValue("token", out StringValues token)
                            )
                            {
                                context.Token = token;
                            }

                            return Task.CompletedTask;
                        },
                        OnAuthenticationFailed = context =>
                        {
                            var timeoutException = context.Exception;
                            return Task.CompletedTask;
                        }
                    };
                });

            return services;
        }

        private static TokenValidationParameters GetTokenValidationParameters(IConfiguration configuration)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Authentication:JwtKey"])),
                ValidateIssuer = true,
                ValidIssuer = configuration["Authentication:JwtIssuer"],
                ValidateAudience = true,
                ValidAudience = configuration["Authentication:JwtAudience"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                NameClaimType = JwtRegisteredClaimNames.UniqueName
            };

            return tokenValidationParameters;
        }
    }
}
