using Microsoft.AspNetCore.Mvc;
using MongoDbApi.Models;
using MongoDbApi.Repository;

namespace MongoDbApi;

public static class PersonModelEndpoints
{
    public static void MapPersonModelEndpoints(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/PersonModel", async ([FromServices] IPersonRepository repo) =>
        {
            return await repo.GetAll();
        })
        .WithName("GetAllPersonModels");

        routes.MapGet("/api/PersonModel/{id}", async (Guid id, [FromServices] IPersonRepository repo) =>
        {
            return await repo.GetOne(id)
                is PersonModel model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetPersonModelById");

        routes.MapPut("/api/PersonModel/{id}", async (Guid id, PersonModel personModel, [FromServices] IPersonRepository repo) =>
        {
            var personFromDb = await repo.GetOne(id);
            if (personFromDb == null)
                return Results.NotFound();

            personModel.Id = personFromDb.Id;
            personModel.InternalId = personFromDb.InternalId;
            await repo.Update(personModel);
            return Results.Ok(personModel);
        })
        .WithName("UpdatePersonModel");

        routes.MapPost("/api/PersonModel/", async (PersonModel personModel, [FromServices] IPersonRepository repo) =>
        {
            personModel.Id = await repo.GetNextId();
            await repo.Create(personModel);

            return Results.Created($"/PersonModels/{personModel.InternalId}", personModel);
        })
        .WithName("CreatePersonModel");

        routes.MapDelete("/api/PersonModel/{id}", async (Guid id, [FromServices] IPersonRepository repo) =>
        {
            var post = await repo.GetOne(id);
            if (post == null)
                return Results.NotFound();
            await repo.Delete(id);
            return Results.Ok();
        })
        .WithName("DeletePersonModel");
    }
}
