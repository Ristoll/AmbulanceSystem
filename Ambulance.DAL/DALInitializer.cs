using AmbulanceSystem.Core.Data;
using Microsoft.Extensions.DependencyInjection;

namespace AmbulanceSystem.DAL;

public static class DALInitializer
{
    public static void AddDataAccessServices(IServiceCollection services)
    {
        services.AddDbContext<AmbulanceDbContext>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
