namespace Sellius.API.Application.DTOs.RegisterDTOs.CustomerRegisterDTOs
{
    public class SegmentacaoDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public short Active { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? AlteredDate { get; set; }
    }
}
