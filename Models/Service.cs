namespace animalhotelAPI.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<Employee> Employees { get; set; }
        public ICollection<Pet> Pets { get; set; }
    }
}
