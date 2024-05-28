using System.Reflection;
using System.Text.Json.Serialization;
using GoalApi.Data;
using GoalApi.Data.Repositories;
using GoalApi.EventHandlers;
using GoalApi.Models;
using LS.Common;
using LS.Events.GoalApi;
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
            options.UseNpgsql(configuration["SelfImprovementDbContext"]);
        });
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(Startup).Assembly));
        services.AddScoped<IGenericRepository<Models.Goal>, GoalRepository>();
        services.AddScoped<IGenericRepository<Models.GoalTask>, GoalTaskRepository>();
        services.Register(configuration);
        services.AddIdentityServices(configuration);
        services.AddHttpClient();
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
        
        app
            .UseCors(options => options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin())
            .UseSwagger(configuration, "Goal");
        app.MapHealthChecks();
        ConfigureEventBusHandlers(app);
    }
    
    private void ConfigureEventBusDependencies(IServiceCollection services)
    {
        services.AddRabbitMqEventBus
        (
            connectionUrl: configuration["RabbitMqConnectionUrl"],
            brokerName:  "eventBusBroker",
            queueName: "eventBusQueue",
            timeoutBeforeReconnecting: 45
        );
        services.AddScoped<TasksForGoalCreatedEventHandler>();
    }

    private void ConfigureEventBusHandlers(IApplicationBuilder app)
    {
        var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
        eventBus.Subscribe<TasksForGoalCreatedEvent, TasksForGoalCreatedEventHandler>();
    }
}