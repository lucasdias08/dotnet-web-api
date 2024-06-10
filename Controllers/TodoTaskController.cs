using FirebaseMedium;
using FireSharp.Response;
using Microsoft.AspNetCore.Mvc;
using TodoTaskApi.Models;
using MongoDB.Bson;
using System.Text.Json.Nodes;

[Microsoft.AspNetCore.Components.Route("api/[controller]")]
[ApiController]
public class TaskController : ControllerBase
{
    ConnectionDB connectionDB = new ConnectionDB();
    // GET api/task
    [HttpGet]
    public string Get()
    {
        try
        {
            FirebaseResponse allTasksResponse = connectionDB.client.Get("tasks");
            JsonNode allTasksFormatted = JsonNode.Parse(allTasksResponse.Body)!;
            List<TodoTask> allTasks = new List<TodoTask>();
            Console.WriteLine(allTasksFormatted?.ToString());

            if (!string.IsNullOrEmpty(allTasksFormatted?.ToString()))
            {
                foreach(var item in (dynamic)allTasksFormatted!)
                {
                    string Id = (string)item.Value["id"];
                    string Title = (string)item.Value["title"];
                    string Description = (string)item.Value["description"];
                    TodoTask todoTask = new TodoTask(Id, Title, Description);                
                    allTasks.Add(todoTask);
                }
            }

            ResponseTask responseTask = new ResponseTask(200, allTasks, null);
            Console.WriteLine("----RETORNANDO TODOS " +DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt")+ "----");
            Console.WriteLine(responseTask.ToJson());
            Console.WriteLine();

            return responseTask.ToJson();
        }
        catch (Exception e)
        {
            ResponseTask responseTask = new ResponseTask(400, null, "Houve um problema no servidor!");
            Console.WriteLine(e);
            return responseTask.ToJson();
        }
    }

    // GET api/task/5
    [HttpGet("{taskId}")]
    public string Get(string taskId)
    {
        try
        {
            FirebaseResponse task = connectionDB.client.Get("tasks/" +taskId);
            Console.WriteLine("---RETORNANDO do ID " +taskId+ " " +DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt")+ "----");
            Console.WriteLine(task.Body.ToString());
            return task.Body.ToString();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Data);
            return "Houve um problema no servidor!";
        }
    }

    // POST api/task
    [HttpPost]
    public string Post([FromBody] TodoTask newTask)
    { 
        try
        {  
            List<TodoTask> allTasks = [new TodoTask(newTask.Id, newTask.Title, newTask.Description)];

            connectionDB.client.Set("tasks/" + newTask.Id + "/id", newTask.Id);
            connectionDB.client.Set("tasks/" + newTask.Id + "/title", newTask.Title);
            connectionDB.client.Set("tasks/" + newTask.Id + "/description", newTask.Description);  

            ResponseTask responseTask = new ResponseTask(200, allTasks, "Tarefa cadastrada com sucesso");
            Console.WriteLine("---CADASTRADO " +DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt")+ "----");
            Console.WriteLine(responseTask.ToJson());

            return responseTask.ToJson(); 
        }
        catch (Exception e)
        {
            ResponseTask responseTask = new ResponseTask(400, null, "Houve um problema no servidor!");
            Console.WriteLine(e);
            return responseTask.ToJson();
        }
    }

    // PUT api/task/5
    [HttpPut("{taskId}")]
    public string Put([FromBody] TodoTask task)
    {   
        try
        {  
            List<TodoTask> allTasks = [new TodoTask(task.Id, task.Title, task.Description)];

            connectionDB.client.Set("tasks/" + task.Id + "/id", task.Id);
            connectionDB.client.Set("tasks/" + task.Id + "/title", task.Title);
            connectionDB.client.Set("tasks/" + task.Id + "/description", task.Description);  

            ResponseTask responseTask = new ResponseTask(200, allTasks, "Tarefa editada com sucesso");
            Console.WriteLine("---EDITADO " +DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt")+ "----");
            Console.WriteLine(responseTask.ToJson());

            return responseTask.ToJson(); 
        }
        catch (Exception e)
        {
            ResponseTask responseTask = new ResponseTask(400, null, "Houve um problema no servidor!");
            Console.WriteLine(e);
            return responseTask.ToJson();
        }
    }

    // DELETE api/task/5
    [HttpDelete("{taskId}")]
    public string Delete(string taskId)
    {
        try
        {
            FirebaseResponse taskForDelete = connectionDB.client.Get("tasks/" +taskId);
            FirebaseResponse task = connectionDB.client.Delete("tasks/" +taskId);

            dynamic allTasksFormatted = JsonNode.Parse(taskForDelete.Body)!;
            string Id = (string)allTasksFormatted["id"];
            string Title = (string)allTasksFormatted["title"];
            string Description = (string)allTasksFormatted["description"];
            List<TodoTask> allTasksDeleted = [new TodoTask(Id, Title, Description)];
            
            ResponseTask responseTask = new ResponseTask(200, allTasksDeleted, "Tarefa " +Title+ " removida com sucesso!");
            Console.WriteLine("----REMOVIDO do ID " +taskId+ " " +DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt")+ "----");
            Console.WriteLine(responseTask.ToJson());
            Console.WriteLine();

            return responseTask.ToJson();
        }
        catch (Exception e)
        {
            ResponseTask responseTask = new ResponseTask(400, null, "Houve um problema no servidor!");
            Console.WriteLine(responseTask.ToJson());
            return responseTask.ToJson();
        }  
    }
}