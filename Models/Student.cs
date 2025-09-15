using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirst.Models;

public class Student
{
    [Key]
    public int Id { get; set; }

    [Column("StudentName", TypeName = "nvarchar(100)")]
    [Required]
    public string Name { get; set; }
    [Column("StudentGender", TypeName = "nvarchar(20)")]
    [Required]
    public string Gender { get; set; }
    [Required]
    public int? Age { get; set; }
    [Required]
    public int? Standard { get; set; }

}
