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
    return taskController.Delete(request.Path.ToString().Split('/')[3]);
})
.WithName("DeleteTaskWithId")
.WithOpenApi();

app.MapPost("/api/task", async (HttpRequest request, HttpResponse response) =>
{
    TodoTask todoTaskBody = await request.ReadFromJsonAsync<TodoTask>();
    TodoTask todoTask = new();

    Guid myuuid = Guid.NewGuid();
    todoTask.Id = myuuid.ToString();
    todoTask.Title = todoTaskBody.Title;
    todoTask.Description = todoTaskBody.Description;

    TaskController taskController = new();
    return taskController.Post(todoTask);
})
.WithName("PostTask")
.WithOpenApi();

app.MapPut("/api/task/{taskId}", async (HttpRequest request, HttpResponse response) =>
{
    TaskController taskController = new();
    TodoTask todoTaskBody = await request.ReadFromJsonAsync<TodoTask>();
    string taskId = request.Path.ToString().Split('/')[3];
    TodoTask todoTask = new(taskId, todoTaskBody.Title, todoTaskBody.Description);

    return taskController.Put(todoTask);
})
.WithName("PutTask")
.WithOpenApi();


app.Run();