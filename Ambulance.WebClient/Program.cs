using Ambulance.BLL;
using Ambulance.ExternalServices;
using Ambulance.WebAPI.Hubs; // <- ������ ��� SignalR Hub
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// --- �������� ��� ������ ---
builder.Services.AddControllers(); // ������ AddControllersWithViews

// --- ������ ��� SignalR ---
builder.Services.AddSignalR();

// --- ���� JWT �������������� ---
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
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTService.secretcode))
    };
});

var app = builder.Build();

// --- Middleware ---
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication(); // ������ ��� JWT
app.UseAuthorization();

// --- �������� --- 
app.MapControllers();

// --- ������ ������� ��� SignalR Hub ---
app.MapHub<NotificationHub>("/notificationHub");

app.Run();
