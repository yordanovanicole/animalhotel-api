namespace animalhotelAPI.DTOs
{
    public record ReservationReadDto( int Id, DateTime StartDate,DateTime EndDate,
 int PetId, int RoomId, int EmployeeId);
    public record ReservationCreateDto(DateTime StartDate, DateTime EndDate,
  int PetId, int RoomId, int EmployeeId);
}
