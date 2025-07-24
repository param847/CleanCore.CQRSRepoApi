using Application.Common.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application.Common.Exceptions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // 1. MediatR handlers
            services.AddMediatR(cfg =>
            {
                // Scan this assembly for IRequestHandler<,>
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            // 2. AutoMapper profiles
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // 3. FluentValidation validators
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // 4. Pipeline: ensure validation runs BEFORE handlers
            services.AddTransient(
                typeof(IPipelineBehavior<,>),
                typeof(ValidationBehavior<,>)
            );

            return services;
        }
    }
}