namespace animalhotelAPI.Models
{
    public class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int OwnerId { get; set; }
        public Owner Owner { get; set; }

        public int PetTypeId { get; set; }
        public PetType PetType { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<Service> Services { get; set; }
    }
}
