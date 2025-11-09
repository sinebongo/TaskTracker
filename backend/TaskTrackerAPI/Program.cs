using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System;
using TaskTrackerAPI.Application.Interfaces;
using TaskTrackerAPI.Application.Services;
using TaskTrackerAPI.Domain.Entities;
using TaskTrackerAPI.Domain.Enums;
using TaskTrackerAPI.Endpoints;
using TaskTrackerAPI.Infrastructure.Database;
using TaskTrackerAPI.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

//Services
builder.Services.AddDbContext<ApplicationDBContext>(x =>
    x.UseInMemoryDatabase("TaskTrackerDB")
);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TaskTracker API",
        Version = "v1"
    });
});
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddCors(x =>
    x.AddDefaultPolicy(p =>
    p.WithOrigins(config["Angular"]?? "http://localhost:4200")
    .AllowAnyHeader()
    .AllowAnyMethod()
        )
);


var app = builder.Build();




//Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(o =>
    {
        o.SwaggerEndpoint("/swagger/v1/swagger.json", "TaskTracker API v1");
        o.RoutePrefix = string.Empty;
    });
}
app.UseCors();
app.UseHttpsRedirection();

//Endpoints
app.MapTaskEndpoints();

//Seed Data 
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();

    if (!db.Tasks.Any())
    {
        db.Tasks.AddRange(
            new TaskItem
            {
                Id = 1,
                Title = "First Task",
                Description = "Review notes for project meeting",
                Status = Status.New,
                Priority = TaskPriority.High,
                DueDate = DateTime.UtcNow.AddDays(2),
                CreatedAt = DateTime.UtcNow
            },
            new TaskItem
            {
                Id = 2,
                Title = "Second Task",
                Description = "Review notes after project meeting",
                Status = Status.InProgress,
                Priority = TaskPriority.Medium,
                DueDate = DateTime.UtcNow.AddDays(1),
                CreatedAt = DateTime.UtcNow
            }
        );

        db.SaveChanges();
    }
}


app.Run();

