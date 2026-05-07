namespace animalhotelAPI.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        public ICollection<Service> Services { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}
