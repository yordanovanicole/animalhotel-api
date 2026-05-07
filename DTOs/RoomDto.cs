namespace animalhotelAPI.DTOs
{
    public record RoomReadDto(int Id, string RoomNumber);
    public record RoomCreateDto( string RoomNumber);
}
