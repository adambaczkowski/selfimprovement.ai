using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace LS.Startup;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwagger(
        this IServiceCollection services,
        IConfiguration configuration,
        string serviceName,
        Action<SwaggerGenOptions> config = null,
        bool addContextHeaderParameters = true,
        bool includeDocs = false)
    {
        return services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = serviceName, Version = "v1" });
            c.IgnoreObsoleteActions();
            c.IgnoreObsoleteProperties();
            // Including docs requires GenerateDocumentationFile option checked
            // for project which using this extension.
            if (includeDocs)
            {
                var xmlFile = $"{Assembly.GetEntryAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            }

            c.OperationFilter<NoOperationIdFilter>();
            if (addContextHeaderParameters)
                c.OperationFilter<AddContextHeaderParameters>();

            var schemaHelper = new SwashbuckleSchemaHelper();
            c.CustomSchemaIds((type) => schemaHelper.GetSchemaId(type));

            config?.Invoke(c);
        });
    }

    public static IApplicationBuilder UseSwagger(this IApplicationBuilder app, IConfiguration configuration, string serviceDisplayName)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", serviceDisplayName);
        });

        return app;
    }
}

public class NoOperationIdFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.OperationId = null;
    }
}

public class AddContextHeaderParameters : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
    }
}