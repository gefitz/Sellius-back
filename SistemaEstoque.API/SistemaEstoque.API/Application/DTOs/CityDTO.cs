namespace Sellius.API.Application.DTOs
{
    public class CityDTO
    {
        public long Id { get; set; }
        public string NameCity { get; set; }
        public StateDTO State { get; set; }
    }
}
