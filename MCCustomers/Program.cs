using System.Text;
using MCCustomers.Models;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<MccustomersContext>();

builder.Services.AddAuthentication()
                .AddJwtBearer(options =>
                {
                    string? appSettingsToken = builder.Configuration.GetSection("AppSettings:Token").Value;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettingsToken!))
                    };
                });

var app = builder.Build();


app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
