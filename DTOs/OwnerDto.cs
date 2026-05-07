namespace animalhotelAPI.DTOs
{
    public record OwnerReadDto(int Id, string FullName);
    public record OwnerCreateDto(string FullName);
}
