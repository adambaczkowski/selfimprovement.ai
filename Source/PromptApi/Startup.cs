using System.Reflection;
using LS.Messaging;
using LS.ServiceClient;
using LS.Startup;
using Microsoft.EntityFrameworkCore;
using PromptApi.Data;

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
        //services.AddMassTransitBus(_configuration, AppDomain.CurrentDomain.GetAssemblies());
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
    }
}