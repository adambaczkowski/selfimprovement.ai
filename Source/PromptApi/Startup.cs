using System.Reflection;
using LS.Messaging;
using LS.ServiceClient;
using LS.Startup;
using Microsoft.EntityFrameworkCore;

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
        services.AddHealthChecks();
        services
            .AddSwagger(_configuration, "identity")
            .AddDefaultCorsPolicy(_configuration["CorsOrigin"])
            .AddHttpContextAccessor();
        services.Register(_configuration);
        services.AddMassTransitBus(_configuration, AppDomain.CurrentDomain.GetAssemblies());
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(WebApplication app, IWebHostEnvironment env)
    {
        app.UseRouting();

        if (!env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        //_container.UseApplicationDatabase<IdentityDbContext>();
        Assembly[] assemblies = { typeof(Startup).Assembly };
        app
            .UseCors("default")
            .UseSwagger(_configuration, "Identity");
        app.MapHealthChecks();
    }
}