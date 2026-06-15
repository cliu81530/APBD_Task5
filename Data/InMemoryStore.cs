using WebApplication2.Models;

namespace WebApplication2.Data;

public static class InMemoryStore
{
    public static List<Room> Rooms = new List<Room>()
    {
        new Room { Id = 1, Name = "Room 1", BuildingCode = "A", Floor = 2,Capacity = 35, HasProjector = true,IsActive = true},
        new Room { Id = 2, Name = "Room 2", BuildingCode = "B",Floor = 2,Capacity = 35, HasProjector = false ,IsActive = false},
        new Room { Id = 3, Name = "Room 3", BuildingCode = "C",Floor = 1,Capacity = 40, HasProjector = false ,IsActive =  true},
        new Room { Id = 4, Name = "Aula C",   BuildingCode = "C", Floor = 0, Capacity = 120,HasProjector = true,  IsActive = true },
        new Room { Id = 5, Name = "Room 110", BuildingCode = "A", Floor = 1, Capacity = 12, HasProjector = false, IsActive = false },
    };

    public static List<Reservation> Reservations = new List<Reservation>()
    {
        new Reservation
        {
            Id = 1, RoomId = 1, OrganizerName = "a",Topic = "Workshop",
            Date = new DateTime(2024,4,30),
            StartTime =  new DateTime(2024,4,30),
            EndTime =  new DateTime(2024,5,10),Status = "Confirmed"
        },
        new Reservation
        {
            Id = 2, RoomId = 1, OrganizerName = "b",Topic = "Intro",
            Date = new DateTime(2026,4,30),
            StartTime =  new DateTime(2026,2,4),
            EndTime =  new DateTime(2026,2,10),Status = "Planned"
        },
        new Reservation { Id = 3, RoomId = 1, OrganizerName = "Anna Kowalska", Topic = "REST Workshop",
            Date = new DateTime(2026, 5, 10), StartTime = new DateTime(2026,5,10,10,0,0),
            EndTime = new DateTime(2026,5,10,12,0,0), Status = "confirmed" },
        new Reservation { Id = 4, RoomId = 3, OrganizerName = "Jan Nowak", Topic = "EF Core Intro",
            Date = new DateTime(2026, 5, 11), StartTime = new DateTime(2026,5,11,14,0,0),
            EndTime = new DateTime(2026,5,11,15,30,0), Status = "planned" },
    };
}