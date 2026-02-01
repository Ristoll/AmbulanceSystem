using Ambulance.BLL.Commands;
using Ambulance.DTO.PersonModels;
using BLL;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Підполковник Бубулюка розпочинає тестування Сер (або Крістіна)");

var services = new ServiceCollection();
services.AddLogging();
BLLInitializer.AddAutoMapperToServices(services);
BLLInitializer.AddCommandDependenciesToServices(services);
IServiceProvider container = services.BuildServiceProvider();

using (var scope = container.CreateScope()) // скоп робиться для кожного http запиту, а тут його немає, тому робимо скоп вручну
{
    var testmanager = scope.ServiceProvider.GetRequiredService<PersonIdentityCommandManager>();

    var dro = new PersonCreateRequest
    {
        Name = "testName",
        Surname = "testSurname",
        MiddleName = "testMiddleName",
        DateOfBirth = DateOnly.FromDateTime(DateTime.Now.AddYears(-20)),
        Gender = 0,
        PhoneNumber = "0988888832",
        Email = "gogog@gm.com",
        Login = "testLogin",
        Password = "testPassword",
        Role = 0
    };

try
    {
        testmanager.CreatePerson(dro);
        var createdperson = testmanager.GetPersonProfile(0);
        Console.WriteLine($"єєєєє, диви, що вийшло -{createdperson.Login}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Помилка: {ex.Message}");
    }
}

Console.WriteLine("Пішов я, бувай після натискання клавіші...");
Console.ReadKey();