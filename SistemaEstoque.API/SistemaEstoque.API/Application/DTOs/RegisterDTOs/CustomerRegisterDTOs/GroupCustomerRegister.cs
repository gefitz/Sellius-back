namespace Sellius.API.Application.DTOs.RegisterDTOs.CustomerRegisterDTOs
{
    public class GroupCustomerRegister
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime AlteredDate { get; set; }
        public short Active { get; set; }
    }
}
