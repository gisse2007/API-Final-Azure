using ClientesAPI.DTOs;
using ClientesAPI.Interfaces;
using ClientesAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClientesAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteRepository _repo;

        public ClientesController(IClienteRepository repo)
        {
            _repo = repo;
        }

        // GET 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteReadDto>>> GetAll()
        {
            var clientes = await _repo.GetAllAsync();

            var dto = clientes.Select(c => new ClienteReadDto
            {
                ClienteId = c.ClienteId,
                Nombre = c.Nombre,
                Email = c.Email,
                Telefono = c.Telefono,
                Direccion = c.Direccion,
                FechaCreacion = c.FechaCreacion,
                FechaActualizacion = c.FechaActualizacion,
                Activo = c.Activo
            });

            return Ok(dto);
        }

        // GET 
        [HttpGet("activos")]
        public async Task<ActionResult<IEnumerable<ClienteReadDto>>> GetActive()
        {
            var clientes = await _repo.GetActiveAsync();

            var dto = clientes.Select(c => new ClienteReadDto
            {
                ClienteId = c.ClienteId,
                Nombre = c.Nombre,
                Email = c.Email,
                Telefono = c.Telefono,
                Direccion = c.Direccion,
                FechaCreacion = c.FechaCreacion,
                FechaActualizacion = c.FechaActualizacion,
                Activo = c.Activo
            });

            return Ok(dto);
        }

        // GET 
        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteReadDto>> GetById(int id)
        {
            var cliente = await _repo.GetByIdAsync(id);

            if (cliente == null)
                return NotFound(new { mensaje = $"Cliente con ID {id} no encontrado" });

            var dto = new ClienteReadDto
            {
                ClienteId = cliente.ClienteId,
                Nombre = cliente.Nombre,
                Email = cliente.Email,
                Telefono = cliente.Telefono,
                Direccion = cliente.Direccion,
                FechaCreacion = cliente.FechaCreacion,
                FechaActualizacion = cliente.FechaActualizacion,
                Activo = cliente.Activo
            };

            return Ok(dto);
        }

        // GET 
        [HttpGet("email/{email}")]
        public async Task<ActionResult<ClienteReadDto>> GetByEmail(string email)
        {
            var cliente = await _repo.GetByEmailAsync(email);

            if (cliente == null)
                return NotFound(new { mensaje = $"Cliente con email {email} no encontrado" });

            var dto = new ClienteReadDto
            {
                ClienteId = cliente.ClienteId,
                Nombre = cliente.Nombre,
                Email = cliente.Email,
                Telefono = cliente.Telefono,
                Direccion = cliente.Direccion,
                FechaCreacion = cliente.FechaCreacion,
                FechaActualizacion = cliente.FechaActualizacion,
                Activo = cliente.Activo
            };

            return Ok(dto);
        }

        // POST 
        [HttpPost]
        public async Task<ActionResult<ClienteReadDto>> Create(ClienteCreateDto dto)
        {
            if (await _repo.EmailExistsAsync(dto.Email))
                return BadRequest(new { mensaje = "Ya existe un cliente con este email." });

            var cliente = new Cliente
            {
                Nombre = dto.Nombre,
                Email = dto.Email,
                Telefono = dto.Telefono,
                Direccion = dto.Direccion
            };

            var nuevo = await _repo.CreateAsync(cliente);

            var response = new ClienteReadDto
            {
                ClienteId = nuevo.ClienteId,
                Nombre = nuevo.Nombre,
                Email = nuevo.Email,
                Telefono = nuevo.Telefono,
                Direccion = nuevo.Direccion,
                FechaCreacion = nuevo.FechaCreacion,
                FechaActualizacion = nuevo.FechaActualizacion,
                Activo = nuevo.Activo
            };

            return CreatedAtAction(nameof(GetById), new { id = nuevo.ClienteId }, response);
        }

        // PUT
        [HttpPut("{id}")]
        public async Task<ActionResult<ClienteReadDto>> Update(int id, ClienteUpdateDto dto)
        {
            var exists = await _repo.ExistsAsync(id);
            if (!exists)
                return NotFound(new { mensaje = $"Cliente con ID {id} no encontrado" });

            if (await _repo.EmailExistsAsync(dto.Email, id))
                return BadRequest(new { mensaje = "Ya existe otro cliente con este email." });

            var clienteEditado = new Cliente
            {
                Nombre = dto.Nombre,
                Email = dto.Email,
                Telefono = dto.Telefono,
                Direccion = dto.Direccion,
                Activo = dto.Activo
            };

            var actualizado = await _repo.UpdateAsync(id, clienteEditado);

            var response = new ClienteReadDto
            {
                ClienteId = actualizado!.ClienteId,
                Nombre = actualizado.Nombre,
                Email = actualizado.Email,
                Telefono = actualizado.Telefono,
                Direccion = actualizado.Direccion,
                FechaCreacion = actualizado.FechaCreacion,
                FechaActualizacion = actualizado.FechaActualizacion,
                Activo = actualizado.Activo
            };

            return Ok(response);
        }

        // PATCH 
        [HttpPatch("{id}/estado")]
        public async Task<ActionResult> CambiarEstado(int id, [FromQuery] bool activo)
        {
            var cliente = await _repo.GetByIdAsync(id);
            if (cliente == null)
                return NotFound(new { mensaje = $"Cliente con ID {id} no encontrado" });

            cliente.Activo = activo;
            cliente.FechaActualizacion = DateTime.Now;

            await _repo.UpdateAsync(id, cliente);

            return Ok(new { mensaje = "Estado actualizado", clienteId = id, activo });
        }

        // DELETE 
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var eliminado = await _repo.DeleteAsync(id);

            if (!eliminado)
                return NotFound(new { mensaje = $"Cliente con ID {id} no encontrado" });

            return Ok(new { mensaje = "Cliente desactivado correctamente", clienteId = id });
        }
    }
}
