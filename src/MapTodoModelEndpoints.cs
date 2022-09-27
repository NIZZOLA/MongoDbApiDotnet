using Microsoft.AspNetCore.Mvc;
using MongoDbApi.Data;
using MongoDbApi.Models;

namespace MongoDbApi;
public static class TodoModelEndpointsClass
{
    public static void MapTodoModelEndpoints(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/TodoModel", async ([FromServices] ITodoRepository repo) =>
        {
            return await repo.GetAll();
        })
        .WithName("GetAllTodoModels");

        routes.MapGet("/api/TodoModel/{id}", async (long id, [FromServices] ITodoRepository repo) =>
        {
            return await repo.GetOne(id)
                is TodoModel model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetTodoModelById");

        routes.MapPut("/api/TodoModel/{id}", async (long id, TodoModel todoModel, [FromServices] ITodoRepository repo) =>
        {
            var todoFromDb = await repo.GetOne(id);
            if (todoFromDb == null)
                return Results.NotFound();
            
            todoModel.Id = todoFromDb.Id;
            todoModel.InternalId = todoFromDb.InternalId;
            await repo.Update(todoModel);
            return Results.Ok(todoModel);
        })
        .WithName("UpdateTodoModel");

        routes.MapPost("/api/TodoModel/", async (TodoModel todoModel, [FromServices] ITodoRepository repo) =>
        {
            todoModel.Id = await repo.GetNextId();
            await repo.Create(todoModel);
            
            return Results.Created($"/TodoModels/{todoModel.InternalId}", todoModel);
        })
        .WithName("CreateTodoModel");

        routes.MapDelete("/api/TodoModel/{id}", async (long id, [FromServices] ITodoRepository repo) =>
        {
            var post = await repo.GetOne(id);
            if (post == null)
                return Results.NotFound();
            await repo.Delete(id);
            return Results.Ok();
        })
        .WithName("DeleteTodoModel");
    }
}
