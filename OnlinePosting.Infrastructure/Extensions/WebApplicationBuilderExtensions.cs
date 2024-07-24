using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlinePosting.Domain.Repository;
using OnlinePosting.Infrastructure.Data;
using OnlinePosting.Infrastructure.Repository;

namespace OnlinePosting.Infrastructure.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static WebApplicationBuilder ConfigureInfrastructureServices(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                                   throw new InvalidOperationException("Connection string 'OnlineDbContext' not found");

            builder.Services.AddDbContext<OnlinePostDbContext>(options => options.UseSqlite(connectionString) );

            builder.Services.AddScoped<IPostEntityRepository, PostEntityRepository>();

            return builder;
        }
    }
}
