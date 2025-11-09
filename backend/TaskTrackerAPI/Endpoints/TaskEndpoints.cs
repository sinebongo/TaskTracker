using TaskTrackerAPI.Application.Interfaces;
using TaskTrackerAPI.Domain.Entities;

namespace TaskTrackerAPI.Endpoints
{
    public static class TaskEndpoints
    {
        public static RouteGroupBuilder MapTaskEndpoints(this IEndpointRouteBuilder endpointRouteBuilder) { 

            var group = endpointRouteBuilder.MapGroup("/api/tasks").WithTags("Tasks");

            group.MapGet("/", async (string? q,string? sort, ITaskService service) =>
            {
                    var tasks = await service.GetAllAsync(q, sort); 
                    return Results.Ok(tasks);
            });

            group.MapGet("/{id}", async (int id, ITaskService service) =>
            {
                var task = await service.GetByIdAsync(id);
                return task is null
                    ? Results.NotFound()
                    : Results.Ok(task);
            });

            group.MapPost("/", async (TaskItem t, ITaskService service) =>
            {
                var created = await service.CreateAsync(t);
                return Results.Created($"/api/tasks/{created.Id}", created);
            });

            group.MapPut("/{id}", async (int id, TaskItem t, ITaskService service) =>
            {
                await service.UpdateAsync(id, t);
                return Results.NoContent();
            });

            group.MapDelete("/{id}", async (int id, ITaskService service) =>
            {
                await service.DeleteAsync(id);
                return Results.NoContent();
            });


            return group;
        }
    }
}
