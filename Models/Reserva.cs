using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientesAPI.Models
{
    public class Reserva
    {
        [Key]
        public int ReservaId { get; set; }

        public DateTime FechaReserva { get; set; } = DateTime.Now;

        [Required]
        public DateTime FechaEntrada { get; set; }   

        [Required]
        public DateTime FechaSalida { get; set; }    

        [Required]
        [StringLength(50)]
        public string Habitacion { get; set; } = string.Empty;  

        [Range(1, 20)]
        public int CantidadPersonas { get; set; } = 1;          

        [Required]
        [StringLength(20)]
        public string Estado { get; set; } = "Pendiente";

        [StringLength(500)]
        public string Descripcion { get; set; } = string.Empty;

        [ForeignKey("Cliente")]
        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }

      
        [ForeignKey("Servicio")]
        public int ServicioId { get; set; }
        public Servicio? Servicio { get; set; }
    }
}
