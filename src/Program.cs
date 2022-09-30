using MongoDbApi;
using MongoDbApi.Data;
using MongoDbApi.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<MongoConfiguration>(builder.Configuration.GetSection("MongoConfiguration"));

builder.Services.AddSingleton<IMongoContext, MongoContext>();
builder.Services.AddSingleton<ITodoRepository,TodoRepository>();
builder.Services.AddSingleton<IPersonRepository, PersonRepository>();

var app = builder.Build();
app.MapTodoModelEndpoints();
app.MapPersonModelEndpoints();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
