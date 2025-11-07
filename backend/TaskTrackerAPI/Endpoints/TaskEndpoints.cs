namespace TaskTrackerAPI.Endpoints
{
    public static class TaskEndpoints
    {
        public static RouteGroupBuilder MapTaskEndpoints(this IEndpointRouteBuilder endpointRouteBuilder) { 

            var group = endpointRouteBuilder.MapGroup("/api/tasks").WithTags("Tasks");

            return group;
        }
    }
}
