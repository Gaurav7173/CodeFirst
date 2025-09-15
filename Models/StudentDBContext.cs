using System;
using Microsoft.EntityFrameworkCore;

namespace CodeFirst.Models;

public class StudentDBContext : DbContext //DbContext class inherits from the Entity Framework Core DbContext class
{
    public StudentDBContext(DbContextOptions options) : base(options) //Constructor that takes DbContextOptions and passes it to the base class constructor and here we are using base keyword to call the parect class constructor
    {


    }
    public DbSet<Student> Students { get; set; } //DbSet property representing the Students table in the database


}
