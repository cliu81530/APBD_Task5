namespace WebApplication2.Controllers;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Data;
using WebApplication2.Models;
[ApiController]
[Route("api/[controller]")]
public class ReservationsController: ControllerBase
{
    // [HttpGet]
    // public ActionResult<List<Reservation>> GetReservations()
    // {
    //     return Ok(InMemoryStore.Reservations);
    // }



    [HttpGet]
    public ActionResult<IEnumerable<Reservation>> GetFiltered(
        [FromQuery] DateTime? date,
        [FromQuery] string? status,
        [FromQuery] int? roomId)
    {
        var query = InMemoryStore.Reservations.AsEnumerable();
        if (date.HasValue)
        {
            query = query.Where(r => r.Date == date.Value);
        }

        if (!string.IsNullOrWhiteSpace(status))
        {
            query = query.Where(r =>r.Status == status);
        }
        if (roomId.HasValue)
        {
            query = query.Where(r => r.RoomId == roomId.Value);
        }
        return Ok(query);
    }
    
    [HttpGet("{id}")]
    public ActionResult<Reservation> GetReservation(int id)
    {
        var reservation = InMemoryStore.Reservations
            .FirstOrDefault(r => r.Id == id);
        if(reservation == null)
        {
            return NotFound();
        }
        return Ok(reservation);
    }

    [HttpPost]
    public ActionResult<Reservation> Create([FromBody] Reservation reservation)
    {
        //room must exist
        var existingRoom = InMemoryStore.Rooms.FirstOrDefault(r => r.Id == reservation.RoomId);
        if (existingRoom is null)
        {
            return NotFound($"Room {reservation.RoomId} does not exist.");
        }
        //room must be active
        if (!existingRoom.IsActive)
        {
            return Conflict("Cannot reserve a room that is not active.");
        }
        //reservation time must be legal
        bool hasConflict = InMemoryStore.Reservations.Any(
            existing => existing.RoomId == reservation.RoomId&&
                        existing.Date == reservation.Date&&
                        existing.StartTime < reservation.EndTime &&
                        existing.EndTime > reservation.StartTime);
        if (hasConflict)
        {
            return Conflict("Reservation time overlap with existing reservation.");
        }
        int newId = InMemoryStore.Reservations.Any() ? InMemoryStore.Reservations.Max(r => r.Id) + 1 : 1;
        reservation.Id = newId;
        
        InMemoryStore.Reservations.Add(reservation);
        return CreatedAtAction(nameof(GetReservation), new { id = reservation.Id }, reservation);
    }

    [HttpPut("{id}")]
    public ActionResult<Reservation> Update(int id, [FromBody] Reservation reservation)
    {
        var existingRoom = InMemoryStore.Reservations.FirstOrDefault(r => r.Id == id);
        if (existingRoom is null)
        {
            return NotFound($"Room {reservation.RoomId} does not exist.");
        }
        existingRoom.RoomId = reservation.RoomId;
        existingRoom.OrganizerName = reservation.OrganizerName;
        existingRoom.Topic = reservation.Topic;
        existingRoom.StartTime = reservation.StartTime;
        existingRoom.EndTime = reservation.EndTime;
        existingRoom.Status = reservation.Status;
        existingRoom.Date = reservation.Date;
        
        return Ok(existingRoom);
    }
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var reservation = InMemoryStore.Reservations.FirstOrDefault(r => r.Id == id);
        if (reservation == null)
        {
            return NotFound();
        }
        InMemoryStore.Reservations.Remove(reservation);
        return NoContent();
    }
}