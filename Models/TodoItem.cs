namespace api.Models;

public class TodoItem
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public bool IsComplete { get; set; }
    public DateTime? CreateDateTime { get; set; } =  DateTime.Now;

}