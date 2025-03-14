using DCIAspNetTemplate.Application.Interfaces;
using DCIAspNetTemplate.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace DCIAspNetTemplate.Presentation;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
      services.AddScoped(typeof(IRepository<,>), typeof(InMemoryRepository<,>));
      
      return services;
    }
}
