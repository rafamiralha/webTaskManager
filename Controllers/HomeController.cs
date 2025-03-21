using Microsoft.AspNetCore.Mvc;
using webTaskManager.Data;
using webTaskManager.Models;
using System.Linq;
public class HomeController : Controller
{
    private readonly AppDbContext _context;

    public HomeController(AppDbContext context)
    {
        _context = context;
    }
    public IActionResult Index()
    {
        var tasks = _context.Tasks.ToList();
        return View(tasks);
    }
    public IActionResult Criar()
    {
        return View();
    }
[HttpPost]
public IActionResult Criar(webTaskManager.Models.Task task)
{
    Console.WriteLine($"Recebido: {task.title_task}");

    if (ModelState.IsValid)
    {
        _context.Tasks.Add(task);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    return View(task);
}
    public IActionResult Editar(int id)
    {
        var task = _context.Tasks.Find(id);
        if (task == null) return NotFound();
        return View(task);
    }

    [HttpPost]
    public IActionResult Editar(webTaskManager.Models.Task task)
    {
        if (ModelState.IsValid)
        {
            _context.Tasks.Update(task);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(task);
    }

    public IActionResult Excluir(int id)
    {
        var task = _context.Tasks.Find(id);
        if (task != null)
        {
            _context.Tasks.Remove(task);
            _context.SaveChanges();
        }
        return RedirectToAction("Index");
    }
}