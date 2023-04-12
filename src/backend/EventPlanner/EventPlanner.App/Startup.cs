using System.Reflection;
using System.Text;
using EventPlanner.App.Services.Interfaces;
using EventPlanner.App.Authentication;
using EventPlanner.App.Filters;
using EventPlanner.App.Settings;
using EventPlanner.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EventPlanner.App;

public class Startup
{
    private IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        var securitySettingsSection = Configuration.GetSection("SecuritySettings");
        services.Configure<SecuritySettings>(securitySettingsSection);
        var securitySettings = securitySettingsSection.Get<SecuritySettings>();

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidIssuer = securitySettings.JwtIssuer,
            ValidAudience = securitySettings.JwtAudience,
            ValidateIssuerSigningKey = true,
                
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securitySettings.JwtSecret)),
            ClockSkew = TimeSpan.Zero // Remove delay after token expires
        };

        services.AddAuthorization();
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = false;

                options.TokenValidationParameters = tokenValidationParameters;

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Is-Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
            });
        
        services.AddControllers(options =>
        {
            options.Filters.Add<DatabaseExceptionFilter>();
        });
        services.AddSingleton(new JwtManager(securitySettings));
        services.AddHttpContextAccessor();
        services.AddScoped<ClaimsValidationService>();
        
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Event planner's API",
                Version = "v1",
                Description = "ASP.NET Core Web API for Event planner app"
            });
            
            c.CustomOperationIds(apiDescription =>
            {
                return apiDescription.TryGetMethodInfo(out MethodInfo methodInfo) ? methodInfo.Name : null;
            });
            
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                BearerFormat = "JWT",
                Scheme = "bearer",
                Description = "Please insert JWT with Bearer into field",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            });

            var securityScheme = new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            };
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { securityScheme, new string[] { } }
            });

            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
            c.IncludeXmlComments(xmlPath);
        });

        var connectionString = Configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrEmpty(connectionString))
        {
            services.AddDbContext<EventPlannerContext>(options => options.UseInMemoryDatabase("EventPlanner"));
        }
        else
        {
            services.AddDbContext<EventPlannerContext>(options => options.UseNpgsql(connectionString));
        }

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddScoped<IParticipantSelectionService, Services.ParticipantSelectionService>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, EventPlannerContext eventPlannerContext)
    {
        eventPlannerContext.Database.Migrate();
        
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Event planner v1");
                c.DisplayOperationId();
            });
        }

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}
