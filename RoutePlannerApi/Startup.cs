using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RoutePlannerApi.Services;
using RoutePlannerApi.Services;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
{
    services.AddControllers().AddNewtonsoftJson();
    services.AddSingleton<RouteService>();
    services.AddSwaggerGen();
    
    services.AddCors(options =>
    {
        options.AddPolicy("AllowAllOrigins",
            builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
    });
}

public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RoutePlannerApi v1"));
    }

    app.UseHttpsRedirection();
    app.UseRouting();

    app.UseCors("AllowAllOrigins");

    app.UseAuthorization();
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });
}
}
