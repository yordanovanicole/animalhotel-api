namespace animalhotelAPI.DTOs
{
    public record ReviewReadDto(int Id, string Text,int ReservationId);

    public record ReviewCreateDto(string Text,int ReservationId);
}
