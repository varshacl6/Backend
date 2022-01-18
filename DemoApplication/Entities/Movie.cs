using System.ComponentModel.DataAnnotations;

namespace DemoApplication.Entities;

public class Movie: BaseEntity
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
}