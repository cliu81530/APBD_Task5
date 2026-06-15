using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models;

public class Reservation
{
    public int Id { get; set; }
    public int RoomId { get; set; }
    [Required]
    public string OrganizerName { get; set; } = null!;
    [Required]
    public string Topic { get; set; } = null!;
    public DateTime Date { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    [Required(ErrorMessage = "Status is required")]
    public string Status { get; set; } = null!;
}