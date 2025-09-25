using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CodeFirst.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> Gender = new()
        {
            new SelectListItem{Value="Male",Text="Male"},
            new SelectListItem{Value="Female",Text="Female"}

        };

        ViewBag.Gender=Gender;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Student std)
    {
        if (ModelState.IsValid)
        {
            await studentDB.Students.AddAsync(std); //Adding the new student entity to the Students DbSet
            await studentDB.SaveChangesAsync(); //Saving changes to the database
            TempData["Insert_Success"] = "Student record inserted successfully"; //Setting a success message in TempData to be displayed after redirection
            return RedirectToAction("Index", "Home"); //Redirecting to the Index action after successful creation
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

    public async Task<IActionResult> Edit(int? id)
    {
         List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> Gender = new()
        {
            new SelectListItem{Value="Male",Text="Male"},
            new SelectListItem{Value="Female",Text="Female"}

        };

        ViewBag.Gender=Gender;
        
        if (id == null || studentDB.Students == null)
        {
            return NotFound();
        }
        var stdData=await studentDB.Students.FindAsync(id); //Retrieving all student records from the Students DbSet and converting them to a list

        if (stdData == null)
        {
            return NotFound();
        }
        return View(stdData);   //Passing the list of students to the view for rendering
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, Student std)
    {
        if (id != std.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            studentDB.Update(std); //Adding the new student entity to the Students DbSet
            await studentDB.SaveChangesAsync();
            TempData["update_Success"] = "Student record updated successfully";
            return RedirectToAction("Index", "Home"); //Redirecting to the Index action after successful creation
        }

        return View(std);
    }

      public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || studentDB.Students == null)
        {
            return NotFound();
        }

        var stdData=await studentDB.Students.FirstOrDefaultAsync(x=>x.Id==id); //Retrieving all student records from the Students DbSet and converting them to a list

        if (stdData == null)
        {
            return NotFound();
        }
        return View(stdData);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var stdData = await studentDB.Students.FindAsync(id);
        if (stdData != null)
        {
            studentDB.Students.Remove(stdData); //removing the new student entity to the Students DbSet

        }
        await studentDB.SaveChangesAsync(); //Saving changes to the database
        TempData["delete_Success"] = "Student record deleted successfully";
        return RedirectToAction("Index", "Home");


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
