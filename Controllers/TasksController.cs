using api.Data;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class TasksController:ControllerBase
{
    
    private readonly TodoDbContext _todoDbContext;
    public TasksController(TodoDbContext todoDbContext)
    {
        _todoDbContext = todoDbContext;
    }

    [HttpGet()]
    public ActionResult<IEnumerable<TodoItem>> Get()
    {
        Console.WriteLine("Get Request!");
        var items = _todoDbContext.TodoItems.ToList();
        return Ok(items);
    }

    [HttpGet("{id}")]
    public ActionResult<TodoItem> Get(int id)
    {
        Console.WriteLine("detail get Request!");
        var findTodoItem = _todoDbContext.TodoItems.FirstOrDefault(data=>data.Id==id);
        if (findTodoItem is null)
        {
            return NotFound();
        }
        return Ok(findTodoItem);
    }

    [HttpPost]
    public ActionResult<TodoItem> Post([FromBody] TodoItem todoItem)
    {
        Console.WriteLine("Post Request!");
        _todoDbContext.TodoItems.Add(todoItem);
        _todoDbContext.SaveChanges();
        return CreatedAtAction(nameof(Get), new { id = todoItem.Id }, todoItem);
    }
    
    [HttpPut("{id}")]
    public ActionResult<TodoItem> Put(int id,[FromBody] TodoItem todoItem)
    {
        Console.WriteLine("Post Request!");
        if (id != todoItem.Id)
        {
            return BadRequest();
        }
        var findTodoItem = _todoDbContext.TodoItems.FirstOrDefault(todo=>todo.Id==id);
        if (findTodoItem is null)
        {
            return NotFound();
        }
        findTodoItem.Title = todoItem.Title;
        findTodoItem.Description = todoItem.Description;
        findTodoItem.IsComplete = todoItem.IsComplete;
        findTodoItem.CreateDateTime=DateTime.Now;
        _todoDbContext.SaveChanges();
        return Ok(findTodoItem);
    }

    [HttpDelete("{id}")]
    public ActionResult<TodoItem> Delete(int id)
    {
        Console.WriteLine("Delete Request!");
        var findTodoItem = _todoDbContext.TodoItems.FirstOrDefault(data=>data.Id==id);
        if (findTodoItem is null)
        {
            return NotFound();
        }
        
        _todoDbContext.TodoItems.Remove(findTodoItem);
        _todoDbContext.SaveChanges();
        return NoContent();
    }
    
}