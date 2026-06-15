using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models;

public class Room
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Room name is required")]
    public string Name { get; set; } = null!;
    [Required(ErrorMessage = "Building code is required")]
    public string BuildingCode { get; set; } = null!;
    public int Floor { get; set; }
    [Range(1, int.MaxValue, ErrorMessage = "capacity must be greater than 0")]
    public int Capacity { get; set; }
    public bool HasProjector { get; set; }
    public bool IsActive { get; set; }
}