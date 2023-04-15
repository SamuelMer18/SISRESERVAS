using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SISRESERVAS.Models
{
    public class reserva
    {
        [Key]

        public int IdRes { get; set; }
        public DateTime FechaReserva { get; set; }

        public int Cantidad { get; set; }


        [ForeignKey("Idchofer")]
        public int choferIdchofer { get; set; }
      
        [ForeignKey("UsuarioId")]
        public int usuarioId { get; set; }
      

        [ForeignKey("IdDep")]
        public int departamentoIdDep { get; set; }
     
        [ForeignKey("idbus")]
        public int busidbus { get; set; }
  }
}