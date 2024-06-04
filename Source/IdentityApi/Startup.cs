using System.Text.Json.Serialization;
using IdentityApi.Data;
using IdentityApi.Identity.Commands;
using IdentityApi.Identity.Commands.TokenProvider;
using IdentityApi.Identity.Services;
using IdentityApi.Models;
using IdentityApi.Services;
using LS.Common;
using LS.ServiceClient;
using LS.Startup;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

namespace IdentityApi;

public class Startup(IConfiguration configuration)
{
    // This method gets called by the runtime. Use this method to add serices to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.Configure<Token>(configuration.GetSection("token"));
        services.AddControllers()
            .AddJsonOptions(x =>
            {
                x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
        services.AddHealthChecks();
        services
            .AddSwagger(configuration, "identity")
            .AddDefaultCorsPolicy(configuration["CorsOrigin"])
            .AddHttpContextAccessor();
        services.AddIdentity<Models.User, IdentityRole>()
            .AddDefaultTokenProviders()
            .AddUserManager<UserManager<Models.User>>()
            .AddSignInManager<SignInManager<Models.User>>()
            .AddEntityFrameworkStores<IdentityDbContext>()
            .AddTokenProvider<EmailConfirmationTokenProvider>("EmailConfirmationTokenProvider");
        services.AddDbContext<IdentityDbContext>(options =>
        {
            options.UseNpgsql(configuration["SelfImprovementDbContext"]);
        });
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(Startup).Assembly));
        services.AddScoped<IIdentityEmailService, IdentityEmailService>();
        services.AddScoped<IGenericRepository<Models.User>, UserRepository>();
        services.AddScoped<IGenericRepository<UserProfile>, UserProfileRepository>();
        services.AddScoped<IEmailSender, EmailSender>();
        services.Register(configuration);
        services.AddIdentityServices(configuration);
        services.AddBlobStorage(configuration);
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
            .UseCors("default")
            .UseSwagger(configuration, "Identity");
        app.MapHealthChecks();
    }
}