
namespace TodoTaskApi.Models;

public class TodoTask
{
    public  TodoTask() {}
    
    public  TodoTask(string Id, string Title, string Description)
    {
        this.Id = Id;
        this.Title = Title;
        this.Description = Description;
    }
    public string Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
}