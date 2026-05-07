namespace animalhotelAPI.Models
{
    public class Extra
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
    }
}
