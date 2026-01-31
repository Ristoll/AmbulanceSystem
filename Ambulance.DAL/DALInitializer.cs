using Ambulance.Core.Entities;
using AmbulanceSystem.Core;
using Microsoft.Extensions.DependencyInjection;

namespace AmbulanceSystem.DAL;

public static class DALInitializer
{
    public static void AddDataAccessServices(IServiceCollection services)
    {
        //services.AddDbContext<AmbulanceDbContext>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // реєстрація сервісу для збереження-клонування зображень
        services.AddSingleton<IImageService>(provider =>
        {
            var basePath = Path.Combine(AppContext.BaseDirectory, "Images");
            return new ImageService("Persons", basePath);
        });
    }
}
