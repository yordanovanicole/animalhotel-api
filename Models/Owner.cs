namespace animalhotelAPI.Models
{
    public class Owner
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        public ICollection<Pet> Pets { get; set; }
    }
}
