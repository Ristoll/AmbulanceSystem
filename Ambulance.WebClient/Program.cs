using Ambulance.BLL;
using Ambulance.ExternalServices;
using Ambulance.WebAPI.Hubs; // <- додано для SignalR Hub
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// --- Залишаємо твої сервіси ---
builder.Services.AddControllers(); // замість AddControllersWithViews

// --- Додано для SignalR ---
builder.Services.AddSignalR();

// --- Твоя JWT аутентифікація ---
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTService.secretcode))
    };
});

var app = builder.Build();

// --- Middleware ---
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication(); // додано для JWT
app.UseAuthorization();

// --- Маршрути --- 
app.MapControllers();

// --- Додано маршрут для SignalR Hub ---
app.MapHub<NotificationHub>("/notificationHub");

app.Run();
