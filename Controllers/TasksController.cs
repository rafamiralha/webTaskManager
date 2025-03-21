using Microsoft.AspNetCore.Mvc;
using webTaskManager.Data;
using webTaskManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;

namespace webTaskManager.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TasksController : ControllerBase 
{
    private readonly AppDbContext _context;

    public TasksController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Models.Task>>> GetTasks()
    {
        return await _context.Tasks.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Models.Task>> GetTask(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if(task == null)
            return NotFound();

        return task;
    }

    [HttpPost]
    public async Task<ActionResult<Models.Task>> PostTask(Models.Task task)
    {
        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetTask), new {id = task.Id}, task);
    }
    [HttpPut("{id}")]
    public async Task<ActionResult<Models.Task>> PutTask(int id, Models.Task task)
    {
        if(id != task.Id)
            return BadRequest();
        
        _context.Entry(task).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult<Models.Task>> DeleteTask(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if(task == null)
        return NotFound();

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}