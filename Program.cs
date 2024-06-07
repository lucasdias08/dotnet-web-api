using TodoTaskApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => 
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });
}

app.UseHttpsRedirection();

app.MapGet("/testingServer", () =>
{
    return new { code = 200, message = "Servidor funcionando!"} ;
})
.WithName("TestingServe")
.WithOpenApi();

app.MapGet("/api/task", () =>
{
    TaskController taskController = new();
    return taskController.Get();
})
.WithName("GetTask")
.WithOpenApi();

app.MapGet("/api/task/{taskId}" , (HttpRequest request, HttpResponse response) =>
{
    TaskController taskController = new();
    return taskController.Get(request.Path.ToString().Split('/')[3]);
})
.WithName("GetTaskWithId")
.WithOpenApi();

app.MapDelete("/api/task/{taskId}" , (HttpRequest request, HttpResponse response) =>
{
    TaskController taskController = new();
    taskController.Delete(request.Path.ToString().Split('/')[3]);
})
.WithName("DeleteTaskWithId")
.WithOpenApi();

app.MapPost("/api/task", (HttpRequest request, HttpResponse response) =>
{
    Console.WriteLine("entrou");
    TodoTask todoTask = new();
    Guid myuuid = Guid.NewGuid();
    todoTask.Id = myuuid.ToString();
    todoTask.Title = "TesteTitle";
    todoTask.Description = "TesteDescription";

    TaskController taskController = new();
    taskController.Post(todoTask);
})
.WithName("PostTask")
.WithOpenApi();

// app.MapPut("/api/task/{taskId}", (HttpRequest request, HttpResponse response) =>
// {
//     TaskController taskController = new();
//     string taskId = taskController.Get(request.Path.ToString().Split('/')[3]);
//     TodoTask todoTask = new();
//     todoTask.Id = taskId;
//     todoTask.Title = "TesteTitle";
//     todoTask.Description = "TesteDescription";

//     taskController.Put(todoTask, taskId);
// })
// .WithName("PutTask")
// .WithOpenApi();

app.Run();