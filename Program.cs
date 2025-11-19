using Microsoft.EntityFrameworkCore;
using Project_Planner_API;
using Project_Planner_API.Data;
using Project_Planner_API.Services;
using Project_Planner_API.Services.Implementations;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connection = builder.Configuration.GetConnectionString("ProjectPlanner");

builder.Services.AddDbContext<DataContext>(options => options.UseNpgsql(connection));

builder.Services.AddTransient<IStudentsService, StudentsServiceImpl>();

builder.Services.AddExceptionHandler<ExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using var serviceScope = app.Services.CreateScope();

var context = serviceScope.ServiceProvider.GetService<DataContext>();

context?.Database.Migrate();

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
