using AmbulanceSystem.Core.Data.Services;
using Microsoft.Extensions.DependencyInjection;
using AmbulanceSystem.Core.Data;

namespace AmbulanceSystem.DAL;

public static class DALInitializer
{
    public static void AddDataAccessServices(IServiceCollection services)
    {
        // реєстрація контексту для автостворення юніту
        services.AddDbContext<AmbulanceDbContext>();

        // реєстрація UnitOfWork як реалізацію IUnitOfWork
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // реєстрація сервісу для збереження-клонування зображень
        services.AddScoped<IImageService>(provider => new ImageService("Images")); // директорією буде Images
    }
}
