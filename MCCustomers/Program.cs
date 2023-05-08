using MCCustomers.Models;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<MccustomersContext>();


var app = builder.Build();



app.UseAuthorization();

app.MapControllers();

app.Run();
