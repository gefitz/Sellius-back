namespace Sellius.API.Domain.Entity
{
    public class LogModel
    {
        public long id { get; set; }
        public string Message { get; set; }
        public string InnerException { get; set; }
        public DateTime ErrorDate { get; set; }

    }
}
