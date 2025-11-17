using Ambulance.BLL.Commands;
using Ambulance.BLL.Commands.AnaliticsCommands;
using Ambulance.BLL.Commands.BigadeCommands;
using Ambulance.BLL.Commands.CallCommands;
using Ambulance.BLL.Commands.ItemCommands;
using Ambulance.BLL.Commands.MedicalCardCommands;
using AmbulanceSystem.DAL;
using Microsoft.Extensions.DependencyInjection;

namespace BLL;

public static class BLLInitializer
{
    public static void AddAutoMapperToServices(IServiceCollection services)
    {
        // реєстрація автомаперу
        services.AddAutoMapper(cfg =>
        {
            cfg.AddMaps(typeof(BLLInitializer).Assembly); // сканує всі профілі в поточній збірці
        });
    }

    public static void AddCommandDependenciesToServices(IServiceCollection services)
    {
        // ініціалізація DAL
        DALInitializer.AddDataAccessServices(services);

        // складні командні менеджери (логіка з окремими командами)
        services.AddScoped<PersonIdentityCommandManager>();
        services.AddScoped<MedicalCardCommandManager>();
        services.AddScoped<ItemCommandManager>();
        services.AddScoped<CallCommandManager>();
        services.AddScoped<BrigadeCommandManager>();
        services.AddScoped<AnaliticsCommandManager>();
    }
}
