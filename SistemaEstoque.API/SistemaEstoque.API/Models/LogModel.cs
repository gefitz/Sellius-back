using System.ComponentModel.DataAnnotations;

namespace Sellius.API.Models
{
    public class LogModel
    {
        public int id { get; set; }
        public string Messagem { get; set; }
        public string InnerExecption { get; set; }
        public DateTime dthErro { get; set; }

    }
}
