using System.Reflection;
using IdentityApi.Data;
using IdentityApi.Identity.Services;
using IdentityApi.Messaging.Http;
using IdentityApi.Models;
using IdentityApi.Services;
using LS.Common;
using LS.Messaging;
using LS.ServiceClient;
using LS.Startup;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

namespace IdentityApi;

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
            .AddSwagger(_configuration, "identity")
            .AddDefaultCorsPolicy(_configuration["CorsOrigin"])
            .AddHttpContextAccessor();
        services.AddIdentity<Models.User, IdentityRole>().AddEntityFrameworkStores<IdentityDbContext>();
        services.AddDbContext<IdentityDbContext>(options =>
        {
            options.UseNpgsql(_configuration.GetConnectionString("SelfImprovementDbContext"));
        });
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(Startup).Assembly));
        services.AddScoped<IIdentityEmailService, IdentityEmailService>();
        services.AddScoped<IGenericRepository<Models.User>, UserRepository>();
        services.AddScoped<IGenericRepository<UserProfile>, UserProfileRepository>();
        services.AddScoped<IEmailSender, EmailSender>();
        services.Register(_configuration);
        services.AddMassTransitBus(_configuration, AppDomain.CurrentDomain.GetAssemblies());
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

        //app.ApplyMigrations();
        // app.UseApplicationDatabase<IdentityDbContext>();
        app
            .UseCors("default")
            .UseSwagger(_configuration, "Identity");
        app.MapHealthChecks();
    }
}