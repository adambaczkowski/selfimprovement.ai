using System.Reflection;
using Azure.Storage.Blobs;
using LS.Events.GoalApi;
using LS.Events.PromptApi;
using LS.Messaging;
using LS.Messaging.EventBus;
using LS.Messaging.Extensions;
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
            options.UseNpgsql(configuration.GetConnectionString("SelfImprovementDbContext"));
        });
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(Startup).Assembly));
        services.Register(configuration);
        services.AddScoped<ITasksCreatorService, TasksCreatorService>();
        services.AddScoped<IPromptBuilderService, PromptBuilderService>();
        services.AddScoped<IGoalApiClient, GoalApiClient>();
        services.AddScoped<IIdentityApiClient, IdentityApiClient>();
        services.AddScoped<IAiModelApiClient, AiModelApiClient>();
        services.AddRabbitMqEventBus(configuration, "eventbus")
            .AddSubscription<GoalCreatedEvent, GoalCreatedEventHandler>();
        //services.AddSingleton<IBlobStorageService, BlobStorageService>();
        //services.AddSingleton(_ => new BlobServiceClient(configuration.GetConnectionString("BlobStorage")));
        //services.AddIdentityServices(configuration);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(WebApplication app, IWebHostEnvironment env)
    {
        app.UseRouting();

        if (!env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        app
            .UseCors("default")
            .UseSwagger(configuration, "Prompt");
        app.MapHealthChecks();
    }
}