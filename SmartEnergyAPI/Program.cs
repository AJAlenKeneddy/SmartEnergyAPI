using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SmartEnergyAPI.Models;
using Microsoft.EntityFrameworkCore;
using SmartEnergyAPI.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var cadcn = builder.Configuration.GetConnectionString("cn1");


builder.Services.AddDbContext<PlataformaEnergeticaContext>(
    opt => opt.UseSqlServer(cadcn));


var jwtSettings = builder.Configuration.GetSection("Jwt");
var secretKey = jwtSettings.GetValue<string>("Secret");
var issuer = jwtSettings.GetValue<string>("Issuer");
var audience = jwtSettings.GetValue<string>("Audience");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false; 
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
            ValidIssuer = issuer,
            ValidAudience = audience,
            ClockSkew = TimeSpan.Zero
        };
    });


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IRecomendacionService, RecomendacionService>();
builder.Services.AddScoped<IRegistroConsumoService, RegistroConsumoService>();


var app = builder.Build();

// Configura el pipeline de solicitudes HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


// Agrega autenticación y autorización
app.UseAuthentication();  // Esta es la clave
app.UseAuthorization();

app.MapControllers();

app.Run();

