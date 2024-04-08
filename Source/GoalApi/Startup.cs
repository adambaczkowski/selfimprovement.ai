using System.Reflection;
using GoalApi.Data;
using GoalApi.Models;
using LS.Common;
using LS.Startup;
using Microsoft.EntityFrameworkCore;

namespace GoalApi;

public class Startup
{
    private readonly IConfiguration _configuration;
    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    // This method gets called by the runtime. Use this method to add serices to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddHealthChecks();
        services
            .AddSwagger(_configuration, "goal")
            .AddDefaultCorsPolicy(_configuration["CorsOrigin"])
            .AddHttpContextAccessor();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(Startup).Assembly));
        services.AddScoped<IGenericRepository<Goal>, GoalRepository>();
        services.AddDbContext<GoalDbContext>(options =>
        {
            options.UseNpgsql(_configuration.GetConnectionString("SelfImprovementDbContext"));
        });
        services.AddAuthorization();
        services.AddAuthentication();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(WebApplication app, IWebHostEnvironment env)
    {
        app.UseRouting();
        app.UseAuthorization();
        app.UseAuthentication();


        if (!env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        
        app
            .UseCors("default")
            .UseSwagger(_configuration, "Goal");
        app.MapHealthChecks();
    }
}