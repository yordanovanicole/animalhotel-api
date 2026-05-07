namespace animalhotelAPI.DTOs
{
    public record EmployeeReadDto(int Id, string FullName);
    public record EmployeeCreateDto(string FullName);
}
