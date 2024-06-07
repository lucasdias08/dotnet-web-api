
namespace TodoTaskApi.Models;

public class ResponseTask
{
    public  ResponseTask() {}
    
    public  ResponseTask(int Code, List<TodoTask> TodoTaskList, string Message)
    {
        this.Code = Code;
        this.TodoTaskList = TodoTaskList;
        this.Message = Message;
    }
    public int Code { get; set; }
    public List<TodoTask>? TodoTaskList { get; set; }
    public string? Message { get; set; }
}