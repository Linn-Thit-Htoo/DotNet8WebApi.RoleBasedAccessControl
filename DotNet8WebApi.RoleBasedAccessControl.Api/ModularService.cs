using DotNet8WebApi.RoleBasedAccessControl.Api.Features.Auth;
using DotNet8WebApi.RoleBasedAccessControl.DbService.AppDbContexts;
using DotNet8WebApi.RoleBasedAccessControl.Repositories.Features.Auth;
using Microsoft.EntityFrameworkCore;

namespace DotNet8WebApi.RoleBasedAccessControl.Api;

public static class ModularService
{
    public static IServiceCollection AddFeatures(
        this IServiceCollection services,
        WebApplicationBuilder builder
    )
    {
        return services
            .AddDbContextService(builder)
            .AddRepositoryService()
            .AddBusinessLogicService()
            .AddJsonService();
    }

    private static IServiceCollection AddDbContextService(
        this IServiceCollection services,
        WebApplicationBuilder builder
    )
    {
        return builder.Services.AddDbContext<AppDbContext>(
            opt =>
            {
                opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                opt.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
            },
            ServiceLifetime.Transient
        );
    }

    private static IServiceCollection AddRepositoryService(this IServiceCollection services)
    {
        return services.AddScoped<IAuthRepository, AuthRepository>();
    }

    private static IServiceCollection AddBusinessLogicService(this IServiceCollection services)
    {
        return services.AddScoped<BL_Auth>();
    }

    //private static IServiceCollection AddAuthenticationService(this IServiceCollection services)
    //{

    //}

    private static IServiceCollection AddJsonService(this IServiceCollection services)
    {
        services
            .AddControllers()
            .AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.PropertyNamingPolicy = null;
            });

        return services;
    }
}
