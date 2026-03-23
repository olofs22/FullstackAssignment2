using FluentValidation;
using FluentValidation.AspNetCore;
using FullstackAssignment2.Data;
using FullstackAssignment2.Services;
using FullstackAssignment2.Validators;
using Microsoft.AspNetCore.Diagnostics;
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

builder.Services.AddValidatorsFromAssemblyContaining<CreateCarValidator>();

builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddControllers();

builder.Services.AddScoped<CarService>();

builder.Services.AddOpenApi();

var app = builder.Build();

app.UseExceptionHandler(err => err.Run(async ctx =>
{
    var ex = ctx.Features.Get<IExceptionHandlerFeature>()?.Error;
    var (status, message) = ex switch
    {
        KeyNotFoundException => (404, ex.Message),
        ArgumentException => (400, ex.Message),
        _ => (500, "An unexpected error occurred")
    };
    ctx.Response.StatusCode = status;
    await ctx.Response.WriteAsJsonAsync(new { error = message });
}));

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
