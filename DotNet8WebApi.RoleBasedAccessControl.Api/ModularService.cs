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
            .AddJsonService()
            .AddCustomService()
            .AddSwaggerAuthorizationService(builder)
            .AddCorsPolicyService(builder)
            .AddAuthenticationService(builder)
            .AddRepositoryService()
            .AddBusinessLogicService();
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

    private static IServiceCollection AddAuthenticationService(
        this IServiceCollection services,
        WebApplicationBuilder builder
    )
    {
        builder
            .Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
                    )
                };
            });

        return services;
    }

    private static IServiceCollection AddSwaggerAuthorizationService(
        this IServiceCollection services,
        WebApplicationBuilder builder
    )
    {
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc(
                "v1",
                new OpenApiInfo { Title = "Role Based Access Control", Version = "v1" }
            );
            c.AddSecurityDefinition(
                "Bearer",
                new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                }
            );
            c.AddSecurityRequirement(
                new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                }
            );
        });

        builder.Services.AddAuthorization(opt =>
        {
            opt.AddPolicy(
                "AdminOnly",
                policy =>
                {
                    policy.RequireRole(EnumUserRole.Admin.ToString().Encrypt());
                }
            );
        });

        return services;
    }

    private static IServiceCollection AddCorsPolicyService(
        this IServiceCollection services,
        WebApplicationBuilder builder
    )
    {
        return builder.Services.AddCors();
    }

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

    private static IServiceCollection AddCustomService(this IServiceCollection services)
    {
        return services.AddScoped<JWTAuth>().AddScoped<AesService>();
    }

    public static IApplicationBuilder AddMiddleware(this IApplicationBuilder app)
    {
        return app.UseMiddleware<AuthenticationMiddleware>();
    }
}
