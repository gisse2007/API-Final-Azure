using ClientesAPI.DTOs;
using ClientesAPI.Interfaces;
using ClientesAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClientesAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ServiciosController : ControllerBase
    {
        private readonly IServicioRepository _repository;

        public ServiciosController(IServicioRepository repository)
        {
            _repository = repository;
        }

        // GET: api/servicio
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var servicios = await _repository.GetAllAsync();

            var dtoList = servicios.Select(s => new ServicioDto
            {
                ServicioId = s.ServicioId,
                Nombre = s.Nombre,
                Descripcion = s.Descripcion,
                Precio = s.Precio,
                DuracionMinutos = s.DuracionMinutos,
                Activo = s.Activo,
                FechaCreacion = s.FechaCreacion
            });

            return Ok(dtoList);
        }

        // GET: api/servicio/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var servicio = await _repository.GetByIdAsync(id);
            if (servicio == null) return NotFound();

            var dto = new ServicioDto
            {
                ServicioId = servicio.ServicioId,
                Nombre = servicio.Nombre,
                Descripcion = servicio.Descripcion,
                Precio = servicio.Precio,
                DuracionMinutos = servicio.DuracionMinutos,
                Activo = servicio.Activo,
                FechaCreacion = servicio.FechaCreacion
            };

            return Ok(dto);
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ServicioCreateDto dto)
        {
            var servicio = new Servicio
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                Precio = dto.Precio,
                DuracionMinutos = dto.DuracionMinutos,
                Activo = dto.Activo,
                FechaCreacion = DateTime.UtcNow
            };

            var created = await _repository.CreateAsync(servicio);

            return CreatedAtAction(nameof(GetById), new { id = created.ServicioId }, created);
        }

        // PUT
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ServicioUpdateDto dto)
        {
            var updateData = new Servicio
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                Precio = dto.Precio,
                DuracionMinutos = dto.DuracionMinutos,
                Activo = dto.Activo
            };

            var updated = await _repository.UpdateAsync(id, updateData);

            if (updated == null) return NotFound();

            return Ok(updated);
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);

            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}
