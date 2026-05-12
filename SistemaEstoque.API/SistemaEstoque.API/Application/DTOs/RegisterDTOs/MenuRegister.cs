namespace Sellius.API.Application.DTOs.RegisterDTOs
{
    public class MenuRegister
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string? UrlMenu { get; set; }
        public string Icon { get; set; }
        public long? MenuFatherId { get; set; }
        public short? Exclusive { get; set; }
        public short Active { get; set; }
        public DateTime? DateCreate { get; set; }
        public DateTime? DateAltered { get; set; }

        public List<long>? Menu { get; set; }
    }
}
