using Infrastructure.Extensions;
using Presentation.Api.Extensions;
using Presentation.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Infrastructure & Presentation registrations
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddPresentationServices(builder.Configuration);

var app = builder.Build();

// Use CORS before anything that accepts requests
app.UseCors("DefaultCorsPolicy");

// In Development we might still want the developer page:
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MySolution API v1");
        c.RoutePrefix = string.Empty; // serve at root
    });
}
else
{
    // Our middleware for Production
    app.UseMiddleware<ExceptionMiddleware>();
}

// routing + HTTPS
app.UseHttpsRedirection();

// authentication & authorization
app.UseAuthentication();
app.UseAuthorization();

// Global Exception Middleware
app.UseMiddleware<ExceptionMiddleware>();

// Map controllers
app.MapControllers();

app.Run();