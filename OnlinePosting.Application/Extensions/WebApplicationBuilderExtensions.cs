using AspNetCoreRateLimit;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using OnlinePosting.Application.Exceptions;
using OnlinePosting.Application.Shared.AutoMapper;
using OnlinePosting.Application.Shared.Validation;
using System.Reflection;

namespace OnlinePosting.Application.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static WebApplicationBuilder ConfigureApplicationServices(this WebApplicationBuilder builder)
        {
            //IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
            //builder.Services.AddSingleton(mapper);

            //builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

            builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            builder.Services.AddMediatR(conf =>
            {
                conf.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());

                //Configure validation
                //conf.AddBehavior(typeof(IPipelineBehavior<,>),typeof(ValidationBehaviour<,>));
            });

            builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour2<,>));

            //Note instead of registering all validation behaviours independently, i will use reflection to register them 
            builder.Services.AddValidatorsFromAssembly(typeof(WebApplicationBuilderExtensions).Assembly);


            builder.Services.AddExceptionHandler<ExceptionMiddlewareHandler>();
            builder.Services.AddProblemDetails();

            builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));

         

            return builder;
        }
    }
}
