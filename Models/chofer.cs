using System.ComponentModel.DataAnnotations;

namespace SISRESERVAS.Models
{
    public class chofer
    {
        [Key]
        public int Idchofer { get; set; }
        public string Nombrechofer { get; set; }
        
    }
}
