namespace Sellius.API.Application.DTOs.RegisterDTOs
{
    public class LoginRegister
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public Guid UserId { get; set; }
    }
}
