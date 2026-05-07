namespace animalhotelAPI.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }
    }
}
