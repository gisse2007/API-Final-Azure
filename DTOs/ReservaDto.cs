using System.ComponentModel.DataAnnotations;

namespace ClientesAPI.DTOs
{

    public class ReservaCreateDto
    {
        public DateTime FechaProgramada { get; set; }
        public int ClienteId { get; set; }
        public int ServicioId { get; set; }
        public string? Descripcion { get; set; }
    }

    public class ReservaUpdateDto
    {
        public DateTime FechaProgramada { get; set; }
        public int ClienteId { get; set; }
        public int ServicioId { get; set; }
        public string Estado { get; set; } = "Pendiente";
        public string? Descripcion { get; set; }
    }

    public class ReservaReadDto
    {
        public int Id { get; set; }
        public DateTime FechaReserva { get; set; }
        public DateTime FechaProgramada { get; set; }
        public string Estado { get; set; }
        public string? Descripcion { get; set; }

        public int ClienteId { get; set; }
        public string ClienteNombre { get; set; }

        public int ServicioId { get; set; }
        public string ServicioNombre { get; set; }
        public decimal ServicioPrecio { get; set; }
    }
}
