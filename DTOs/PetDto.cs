namespace animalhotelAPI.DTOs
{
    public record PetReadDto(int Id, string Name,int OwnerId,int PetTypeId);
    public record PetCreateDto(string Name, int OwnerId, int PetTypeId );
}
