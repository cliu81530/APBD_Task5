namespace WebApplication2.Controllers;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using WebApplication2.Data;

[ApiController]
[Route("api/[controller]")]
public class RoomsController: ControllerBase
{
    // [HttpGet]
    // public ActionResult<IEnumerable<Rooms>> GetAllRooms()
    // {
    //     return Ok(InMemoryStore.Rooms);
    // }
    [HttpGet]
    public ActionResult<IEnumerable<Room>> GetFiltered(
        [FromQuery] int? minCapacity,
        [FromQuery] bool? hasProjector,
        [FromQuery] bool? activeOnly)
    {
        var query = InMemoryStore.Rooms.AsEnumerable();

        if (minCapacity.HasValue)
        {
            query = query.Where(r => r.Capacity >= minCapacity.Value);
        }

        if (hasProjector.HasValue)
        {
            query = query.Where(r => r.HasProjector == hasProjector.Value);
        }

        if (activeOnly.HasValue)
        {
            query = query.Where(r => r.IsActive == activeOnly.Value);
        }
        return Ok(query);
    }

    [HttpGet("building/{buildingCode}")]
    public ActionResult<IEnumerable<Room>> GetRoomsByBuildingCode(string buildingCode)
    {
        var rooms = InMemoryStore.Rooms
            .Where(r => r.BuildingCode .Equals(buildingCode, StringComparison.OrdinalIgnoreCase));

        return Ok(rooms);
    }

    
    
    [HttpGet("{id}")]
    public ActionResult<Room> GetRoom(int id)
    {
        var room = InMemoryStore.Rooms.FirstOrDefault(
            r => r.Id == id);
        if (room == null)
        {
            return NotFound("Room not found");
        }
        return Ok(room);
    }

    [HttpPost]
    public ActionResult<Room> Post([FromBody] Room room)
    {
        int newId = InMemoryStore.Rooms.Any()? InMemoryStore.Rooms.Max(r => r.Id) + 1 : 1;
        room.Id = newId;
        
        InMemoryStore.Rooms.Add(room);
        return CreatedAtAction(nameof(GetRoom),new { id = room.Id }, room);
    }

    [HttpPut("{id}")]
    public ActionResult<Room> Update(int id, [FromBody] Room room)
    {
        var exsitingRoom = InMemoryStore.Rooms.
            FirstOrDefault(r => r.Id == id);
        if (exsitingRoom == null)
        {
            return NotFound("Room not found");
        }
        exsitingRoom.Name = room.Name;
        exsitingRoom.Capacity = room.Capacity;
        exsitingRoom.IsActive = room.IsActive;
        exsitingRoom.BuildingCode = room.BuildingCode;
        exsitingRoom.Floor = room.Floor;
        
        return Ok(exsitingRoom);
    }


    [HttpDelete("{id}")]
    public ActionResult DeleteRoom(int id)
    {
        var room = InMemoryStore.Rooms.FirstOrDefault(r => r.Id == id);
        if (room == null)
        {
            return NotFound();
        }
        bool hasReservation = InMemoryStore.Reservations.Any(r => r.RoomId == id);
        if (hasReservation)
        {
            return Conflict("Reservation already exists, cannot delete");
        }
        InMemoryStore.Rooms.Remove(room);
        return NoContent();
    }
}