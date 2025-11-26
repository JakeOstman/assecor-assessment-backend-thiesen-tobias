namespace FavoriteColorApi.DTOs
{
    public class PersonDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string LastName { get; set; }
        public required string ZipCode { get; set; }
        public required string City { get; set; }
        public required string Color { get; set; }
    }

}
