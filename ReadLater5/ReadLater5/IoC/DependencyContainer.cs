using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ReadLater5.Application.Inputs.Validators.BookmarkValidators;
using ReadLater5.Application.Interfaces;
using ReadLater5.Application.Services.BookmarkService;
using ReadLater5.Infrastructure.Persistance;
using ReadLater5.Infrastructure.Security;
using ReadLater5.Presentation.Filters;
using System;
using System.Text;

namespace ReadLater5.Presentation.IoC
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddContextDependency(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<ReadLaterDataContext>(opt =>
            {
                opt.UseLazyLoadingProxies();
                opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

        public static IServiceCollection AddIdentityDependency(this IServiceCollection services)
        {
            var builder = services.AddDefaultIdentity<IdentityUser>();
            var identityBuilder = new IdentityBuilder(builder.UserType, builder.Services);
            identityBuilder.AddEntityFrameworkStores<ReadLaterDataContext>();

            return services;
        }

        public static IServiceCollection AddInterfaceDependency(this IServiceCollection services)
        {
            services.AddScoped<IDataContext, ReadLaterDataContext>();
            services.AddScoped<IJwtGenerator, JwtGenerator>();
            return services;
        }

        public static IServiceCollection AddServiceDependency(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(typeof(GetBookmarks.Handler).Assembly);
            services.AddAutoMapper(typeof(GetBookmarks.Handler).Assembly);

            return services;
        }

        public static IServiceCollection AddAuthorizationDependency(this IServiceCollection services)
        {
            services.AddMvc(opt =>
                {
                    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                    opt.Filters.Add(new AuthorizeFilter(policy));
                    opt.Filters.Add(typeof(ActionFilter));
                })
                .AddFluentValidation(cfg =>
                {
                    cfg.RegisterValidatorsFromAssemblyContaining<BookmarkCreateValidator>();
                });

            return services;
        }

        public static IServiceCollection AddSecurityDependency(this IServiceCollection services, IConfiguration configuration)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenKey"]));

            services
                .AddAuthentication()
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key,
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                });

            return services;
        }
    }
}
