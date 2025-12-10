using Microsoft.AspNetCore.Mvc;
using ClientesAPI.DTOs;
using ClientesAPI.Interfaces;
using ClientesAPI.Models;

namespace ClientesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;

        public AuthController(IAuthRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (await _repo.UserExistsAsync(dto.Email))
                return BadRequest("El correo ya está registrado");

            var usuario = new Usuario { Nombre = dto.Nombre, Email = dto.Email };
            var registro = await _repo.RegisterAsync(usuario, dto.Password);

            return Ok(new { mensaje = "Usuario registrado", registro.UsuarioId });
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserReadDto>> Login(LoginDto dto)
        {
            var user = await _repo.LoginAsync(dto.Email, dto.Password);
            if (user == null) return Unauthorized("Credenciales incorrectas");

            return new UserReadDto
            {
                UsuarioId = user.UsuarioId,
                Nombre = user.Nombre,
                Email = user.Email,
                Token = _repo.CreateToken(user)
            };
        }
    }
}

