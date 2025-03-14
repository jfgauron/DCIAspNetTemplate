using DCIAspNetTemplate.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DCIAspNetTemplate.Presentation;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
      return services;
    }
}
