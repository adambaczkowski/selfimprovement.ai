using System.Reflection;
using Azure.Storage.Blobs;
using LS.Common;
using LS.Events.GoalApi;
using LS.Messaging;
using LS.Messaging.EventBus;
using LS.ServiceClient;
using LS.Startup;
using Microsoft.EntityFrameworkCore;
using PromptApi.AI;
using PromptApi.Data;
using PromptApi.EventHandlers;
using PromptApi.ServiceClients;
using PromptApi.Services;

namespace PromptApi;

public class Startup(IConfiguration configuration)
{
    // This method gets called by the runtime. Use this method to add serices to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddHealthChecks();
        services
            .AddSwagger(configuration, "prompt")
            .AddDefaultCorsPolicy(configuration["CorsOrigin"])
            .AddHttpContextAccessor();
        services.AddDbContext<PromptDbContext>(options =>
        {
            options.UseNpgsql(configuration["SelfImprovementDbContext"]);
        });
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(Startup).Assembly));
        services.Register(configuration);
        ConfigureEventBusDependencies(services);
        services.AddScoped<ITasksCreatorService, TasksCreatorService>();
        services.AddScoped<IPromptBuilderService, PromptBuilderService>();
        services.AddScoped<IGoalApiClient, GoalApiClient>();
        services.AddScoped<IIdentityApiClient, IdentityApiClient>();
        services.AddScoped<IAiModelApiClient, AiModelApiClient>();
        services.AddHttpClient();
        services.AddIdentityServices(configuration);
        services.AddBlobStorage(configuration);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(WebApplication app, IWebHostEnvironment env)
    {
        app.UseRouting();

        if (!env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        
        Assembly[] assemblies = { typeof(Startup).Assembly };
        app
            .UseCors("default")
            .UseSwagger(configuration, "Prompt");
        app.MapHealthChecks();
        ConfigureEventBusHandlers(app);
    }
    
    private void ConfigureEventBusDependencies(IServiceCollection services)
    {
        services.AddRabbitMqEventBus
        (
            connectionUrl: configuration["RabbitMqConnectionUrl"],
            brokerName: "eventBusBroker",
            queueName: "eventBusQueue",
            timeoutBeforeReconnecting: 45
        );
        services.AddScoped<GoalCreatedEventHandler>();
    }

    private void ConfigureEventBusHandlers(IApplicationBuilder app)
    {
        var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
        eventBus.Subscribe<GoalCreatedEvent, GoalCreatedEventHandler>();
    }
}