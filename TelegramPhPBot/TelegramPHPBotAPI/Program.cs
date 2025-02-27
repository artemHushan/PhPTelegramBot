using Microsoft.EntityFrameworkCore;
using TelegramPHPBotAPI;
using TelegramPHPBotAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<TelegramPHPBotContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<UserSessionService>();
builder.Services.AddScoped<SecretGeneratorService>();
builder.Services.AddScoped<PHPScriptService>();
builder.Services.AddScoped<UploadPHPScriptService>();
builder.Services.AddScoped<AdminService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
