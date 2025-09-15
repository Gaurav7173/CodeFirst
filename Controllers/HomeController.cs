using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CodeFirst.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeFirst.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly StudentDBContext studentDB; //Private readonly field to hold the injected StudentDBContext instance

    public HomeController(StudentDBContext studentDB)
    {
        this.studentDB = studentDB;
    }

    public async Task<IActionResult> Index()
    {
        var stdData=await studentDB.Students.ToListAsync(); //Retrieving all student records from the Students DbSet and converting them to a list
        return View(stdData); //Passing the list of students to the view for rendering
        return View();
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Student std)
    {
        if(ModelState.IsValid)
        {
            await studentDB.Students.AddAsync(std); //Adding the new student entity to the Students DbSet
            await studentDB.SaveChangesAsync(); //Saving changes to the database
            return RedirectToAction("Index","Home"); //Redirecting to the Index action after successful creation
        }
        return View(std); //If the model state is not valid, return the view with the current student data to display validation errors
    }

    public async Task<IActionResult> Details(int? id )
    {
        if (id == null || studentDB.Students == null)
        {
            return NotFound();
        }
       
        var stdData=await studentDB.Students.FirstOrDefaultAsync(x=>x.Id==id); //Retrieving all student records from the Students DbSet and converting them to a list
            if(stdData==null)
            {
                return NotFound();
            }
        return View(stdData); //Passing the list of students to the view for rendering
        return View();
    }
    
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
