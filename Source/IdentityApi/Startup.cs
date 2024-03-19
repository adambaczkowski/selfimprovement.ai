using System.Reflection;
using IdentityApi.Data;
using IdentityApi.Identity.Services;
using IdentityApi.Messaging.Http;
using IdentityApi.Models;
using IdentityApi.Services;
using LS.Startup;
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
        services.AddHttpClient<IPromptClient, HttpPromptClient>();
        services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<IdentityDbContext>();
        services.AddDbContext<IdentityDbContext>(options =>
        {
            options.UseNpgsql(_configuration.GetConnectionString("SelfImprovementDbContext"));
        });
        services.AddScoped<IIdentityEmailService, IdentityEmailService>();
        services.AddScoped<IEmailSender, EmailSender>();
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

        app.ApplyMigrations();
        // app.UseApplicationDatabase<IdentityDbContext>();
        app
            .UseCors("default")
            .UseSwagger(_configuration, "Identity");
        app.MapHealthChecks();
    }
}