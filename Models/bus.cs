using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SISRESERVAS.Models
{
    public class bus
    {
        [Key]
        public int idbus { get; set; }
        public string nombrebus { get; set; }
    }


}
