using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ClientesAPI.Models
{
    public class Servicio
    {
        [Key]
        public int ServicioId { get; set; }

        [Required, StringLength(120)]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(300)]
        public string Descripcion { get; set; } = string.Empty;

        [Range(0, 999999)]
        public decimal Precio { get; set; }

        [Range(1, 600)]
        public int DuracionMinutos { get; set; } = 60;

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        public bool Activo { get; set; } = true;

        // Relación
        [JsonIgnore]
        public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
    }
}
