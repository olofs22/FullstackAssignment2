using FullstackAssignment2.Data;
using FullstackAssignment2.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
        .WithOrigins("http://localhost:5249", "http://localhost:7148")
        .AllowAnyMethod()
        .AllowAnyHeader(); ;
    });
});
builder.Services.AddControllers();

builder.Services.AddScoped<CarService>();

builder.Services.AddOpenApi();

var app = builder.Build();

app.UseDefaultFiles();

app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("AllowFrontend");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
