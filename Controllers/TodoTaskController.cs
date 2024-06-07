using FirebaseMedium;
using FireSharp.Response;
using Microsoft.AspNetCore.Mvc;
using TodoTaskApi.Models;
using Newtonsoft.Json;
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
            JsonNode allTasksFormatted = JsonNode.Parse(allTasksResponse.Body);
            List<TodoTask> allTasks = new List<TodoTask>();
    
            foreach(var item in (dynamic)allTasksFormatted)
            {
                string Id = (string)item.Value["id"];
                string Title = (string)item.Value["title"];
                string Description = (string)item.Value["description"];
                TodoTask todoTask = new TodoTask(Id, Title, Description);                
                allTasks.Add(todoTask);
            }

            ResponseTask responseTask = new ResponseTask(200, allTasks, null);
            Console.WriteLine("----Retornando todos----");
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
    public ActionResult<string> Get(string taskId)
    {
        try
        {
            FirebaseResponse task = connectionDB.client.Get("tasks/" +taskId);
            Console.WriteLine("----Retornando do ID " +taskId+ "----");
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
    public void Post([FromBody] TodoTask newTask)
    { 
        try
        {  
            connectionDB.client.Set("tasks/" + newTask.Id + "/id", newTask.Id);
            connectionDB.client.Set("tasks/" + newTask.Id + "/title", newTask.Title);
            connectionDB.client.Set("tasks/" + newTask.Id + "/description", newTask.Description);
            
            var jsonFormatted = new { Id = newTask.Id,Title = newTask.Title, Description = newTask.Description };   
            Console.WriteLine("---CADASTRADO----");
            Console.WriteLine(jsonFormatted);  
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Data);
        }
    }

    // PUT api/task/5
    [HttpPut("{taskId}")]
    public void Put([FromBody] TodoTask newTask, string taskId)
    {   
        try
        { 
            Console.WriteLine("oi"); 
            // connectionDB.client.Set("tasks/" + newTask.Id + "/id", newTask.Id);
            // connectionDB.client.Set("tasks/" + newTask.Id + "/title", newTask.Title);
            // connectionDB.client.Set("tasks/" + newTask.Id + "/description", newTask.Description);
            
            // var jsonFormatted = new { Id = newTask.Id,Title = newTask.Title, Description = newTask.Description };   
            // Console.WriteLine("---CADASTRADO----");
            // Console.WriteLine(jsonFormatted);  
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Data);
        }
    }

    // DELETE api/task/5
    [HttpDelete("{taskId}")]
    public void Delete(string taskId)
    {
        try
        {
            FirebaseResponse task = connectionDB.client.Delete("tasks/" +taskId);
            Console.WriteLine("----Removido o iten de ID " +taskId+ "----");
            Console.WriteLine(task.Body.ToString());
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Data);
        }  
    }
}