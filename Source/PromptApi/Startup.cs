using System.Reflection;
using Azure.Storage.Blobs;
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
            .AddSwagger(_configuration, "prompt")
            .AddDefaultCorsPolicy(_configuration["CorsOrigin"])
            .AddHttpContextAccessor();
        services.AddDbContext<PromptDbContext>(options =>
        {
            options.UseNpgsql(_configuration.GetConnectionString("SelfImprovementDbContext"));
        });
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(Startup).Assembly));
        services.Register(_configuration);
        ConfigureEventBusDependencies(services);
        services.AddScoped<ITasksCreatorService, TasksCreatorService>();
        services.AddScoped<IPromptBuilderService, PromptBuilderService>();
        services.AddScoped<IGoalApiClient, GoalApiClient>();
        services.AddScoped<IIdentityApiClient, IdentityApiClient>();
        services.AddScoped<IAiModelApiClient, AiModelApiClient>();
        services.AddSingleton<IBlobStorageService, BlobStorageService>();
        services.AddSingleton(_ => new BlobServiceClient(_configuration.GetConnectionString("BlobStorage")));
        services.AddHttpClient();
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
            .UseSwagger(_configuration, "Prompt");
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
        services.AddTransient<GoalCreatedEventHandler>();
    }

    private void ConfigureEventBusHandlers(IApplicationBuilder app)
    {
        var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
        eventBus.Subscribe<GoalCreatedEvent, GoalCreatedEventHandler>();
    }
}