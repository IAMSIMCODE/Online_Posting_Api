using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Authentication;
using Microsoft.OpenApi.Models;
using OnlinePosting.Application.Authentication;
using OnlinePosting.Application.Extensions;
using OnlinePosting.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Added Layers dependencies
builder.ConfigureInfrastructureServices();
builder.ConfigureApplicationServices();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "basic",
        Type = SecuritySchemeType.Http,
        Description = "Authentication using basic scheme"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "basic"
                }
            },
            new string[] {}
        }
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//app.UseIpRateLimiting();
app.UseExceptionHandler();

app.MapControllers();

app.Run();
