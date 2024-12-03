using Arahk.Application.Repository;
using Arahk.Domain.Identity.Repositories;
using Arahk.Domain.Membership.Repositories;
using Arahk.Infrastructure.Data;
using Arahk.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Arahk.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ArahkDbContext>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IUserProfileRepository, UserProfileRepository>();
        
        return services;
    }
}