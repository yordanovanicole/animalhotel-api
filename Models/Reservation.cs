namespace animalhotelAPI.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int PetId { get; set; }
        public Pet Pet { get; set; }

        public int RoomId { get; set; }
        public Room Room { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public ICollection<Service> Services { get; set; }
        public ICollection<Extra> Extras { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
