using Microsoft.EntityFrameworkCore;
using TaskTrackerAPI.Application.Interfaces;
using TaskTrackerAPI.Application.Services;
using TaskTrackerAPI.Endpoints;
using TaskTrackerAPI.Infrastructure.Database;
using TaskTrackerAPI.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
builder.Services.AddOpenApi();


//Services
builder.Services.AddDbContext<ApplicationDBContext>(x =>
    x.UseInMemoryDatabase("TaskTrackerDB")
);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();
app.UseHttpsRedirection();

//Endpoints
app.MapTaskEndpoints();

app.Run();

