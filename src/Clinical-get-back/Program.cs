using ClinicalBackend.Persistence;
using ClinicalBackend.Presentation.Controllers;
using Microsoft.AspNetCore.Mvc;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplication();

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

builder.Services.AddControllers().AddApplicationPart(typeof(BaseApiController).Assembly);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Api Versioning
// Add API Versioning to the Project

#endregion

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    Seed.ApplySeeding(app.Services.CreateScope().ServiceProvider);
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/apply-migration", ([FromServices] IServiceProvider serviceProvider) =>
{
    Seed.ApplySeeding(serviceProvider);

    return Results.Ok();
}).AllowAnonymous();
// Use CORS
app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();