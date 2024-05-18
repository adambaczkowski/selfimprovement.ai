using System.Reflection;
using System.Text.Json.Serialization;
using GoalApi.Data;
using GoalApi.Data.Repositories;
using GoalApi.EventHandlers;
using GoalApi.Models;
using LS.Common;
using LS.Events.PromptApi;
using LS.Messaging;
using LS.Messaging.EventBus;
using LS.ServiceClient;
using LS.Startup;
using Microsoft.EntityFrameworkCore;

namespace GoalApi;

public class Startup(IConfiguration configuration)
{
    // This method gets called by the runtime. Use this method to add serices to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers()
            .AddJsonOptions(x =>
            {
                x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });;
        services.AddHealthChecks();
        services
            .AddSwagger(configuration, "goal")
            .AddDefaultCorsPolicy(configuration["CorsOrigin"])
            .AddHttpContextAccessor();
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        services.AddDbContext<GoalDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("SelfImprovementDbContext"));
        });
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(Startup).Assembly));
        services.AddScoped<IGenericRepository<Models.Goal>, GoalRepository>();
        services.AddScoped<IGenericRepository<Models.GoalTask>, GoalTaskRepository>();
        services.Register(configuration);
        //services.AddIdentityServices(configuration);
        ConfigureEventBusDependencies(services);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(WebApplication app, IWebHostEnvironment env)
    {
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        if (!env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        
        ConfigureEventBusHandlers(app);
        app
            .UseCors("default")
            .UseSwagger(configuration, "Goal");
        app.MapHealthChecks();
    }
    
    private void ConfigureEventBusDependencies(IServiceCollection services)
    {
        var serviceName = configuration["Service"]
            ?.Split('.').First()
            .Replace("http://", string.Empty)
            .Replace("https://", string.Empty);
        
        services.AddRabbitMQEventBus
        (
            connectionUrl: configuration["RabbitMqConnectionUrl"],
            brokerName:  serviceName + "Broker",
            queueName: serviceName + "Queue",
            timeoutBeforeReconnecting: 15
        );
        services.AddTransient<TasksForGoalCreatedEventHandler>();
    }

    private void ConfigureEventBusHandlers(IApplicationBuilder app)
    {
        var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
        eventBus.Subscribe<TasksForGoalCreatedEvent, TasksForGoalCreatedEventHandler>();
    }
}