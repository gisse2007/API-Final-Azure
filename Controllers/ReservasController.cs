using ClientesAPI.DTOs;
using ClientesAPI.Interfaces;
using ClientesAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PRACTICAFINAL.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReservasController : ControllerBase
    {
        private readonly IReservaRepository _repo;

        public ReservasController(IReservaRepository repo)
        {
            _repo = repo;
        }

        // GET api/reservas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reserva>>> GetAll()
        {
            var reservas = await _repo.GetAllAsync();
            return Ok(reservas);
        }

        // GET api/reservas/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Reserva>> GetById(int id)
        {
            var reserva = await _repo.GetByIdAsync(id);

            if (reserva == null)
                return NotFound();

            return Ok(reserva);
        }

        // GET api/reservas/cliente/{clienteId}
        [HttpGet("cliente/{clienteId}")]
        public async Task<ActionResult<IEnumerable<Reserva>>> GetByCliente(int clienteId)
        {
            var reservas = await _repo.GetByClienteIdAsync(clienteId);
            return Ok(reservas);
        }

        // POST api/reservas
        [HttpPost]
        public async Task<ActionResult<Reserva>> Create(Reserva reserva)
        {
            var nueva = await _repo.CreateAsync(reserva);

            return CreatedAtAction(
                nameof(GetById),
                new { id = nueva.ReservaId },  
                nueva
            );
        }

        // PUT api/reservas/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Reserva updated)
        {
            var result = await _repo.UpdateAsync(id, updated);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // PATCH api/reservas/{id}/estado
        [HttpPatch("{id}/estado")]
        public async Task<ActionResult> CambiarEstado(int id, [FromQuery] string estado)
        {
            var reserva = await _repo.GetByIdAsync(id);
            if (reserva == null)
                return NotFound();

            reserva.Estado = estado;

            var result = await _repo.UpdateAsync(id, reserva);
            return Ok(result);
        }

        // DELETE api/reservas/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var eliminado = await _repo.DeleteAsync(id);

            if (!eliminado)
                return NotFound();

            return NoContent();
        }
    }
}
