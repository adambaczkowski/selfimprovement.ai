using System.Reflection;
using GoalApi.Data;
using GoalApi.Data.Repositories;
using GoalApi.Models;
using LS.Common;
using LS.Messaging;
using LS.Messaging.EventBus;
using LS.ServiceClient;
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
        services.AddDbContext<GoalDbContext>(options =>
        {
            options.UseNpgsql(_configuration.GetConnectionString("SelfImprovementDbContext"));
        });
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(Startup).Assembly));
        services.AddScoped<IGenericRepository<Models.Goal>, GoalRepository>();
        services.AddScoped<IGenericRepository<Models.GoalTask>, GoalTaskRepository>();
        services.Register(_configuration);
        services.AddAuthorization();
        services.AddAuthentication();
        ConfigureEventBusDependencies(services);
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
        ConfigureEventBusHandlers(app);
    }
    
    private void ConfigureEventBusDependencies(IServiceCollection services)
    {
        var serviceName = _configuration["Service"]
            ?.Split('.').First()
            .Replace("http://", string.Empty)
            .Replace("https://", string.Empty);
        
        services.AddRabbitMQEventBus
        (
            connectionUrl: _configuration["RabbitMqConnectionUrl"],
            brokerName:  serviceName + "Broker",
            queueName: serviceName + "Queue",
            timeoutBeforeReconnecting: 15
        );
    }

    private void ConfigureEventBusHandlers(IApplicationBuilder app)
    {
        var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
    }
}