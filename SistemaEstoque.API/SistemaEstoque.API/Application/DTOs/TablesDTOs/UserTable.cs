namespace Sellius.API.Application.DTOs.TablesDTOs
{
    public class UserTable
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string TypeUser { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public short Active { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime? AlteredDate { get; set; }
    }
}
