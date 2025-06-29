using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc;
using BackendCine.Services;
using BackendCine.Entities.Models;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Company.TestProject1;
using Microsoft.AspNetCore.Authorization;

namespace BackendCine.Controllers
{
    [ApiController]
    [Route("Auth")]
    public class AuthController : ControllerBase
    {
        private readonly UsuarioService _serviceUsuario;
        private readonly IConfiguration _configuration;

        public AuthController(UsuarioService serviceUsuario, IConfiguration configuration)
        {
            _serviceUsuario = serviceUsuario;
            _configuration = configuration;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginParam param)
        {
            if (string.IsNullOrEmpty(param.NomUsuario) || string.IsNullOrEmpty(param.Password))
            {
                return BadRequest("Username and password cannot be empty.");
            }

            var user = await _serviceUsuario.AuthenticateAsync(param);
            if (user == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            // Crear el token
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.NomUsuario),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new
            {
                token = tokenString,
                user = new { user.Id, user.NomUsuario, user.IdCliente }
            });
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] Usuario usuario)
        {
            await _serviceUsuario.AddUsuarioAsync(usuario);
            return CreatedAtAction(nameof(Login), new { id = usuario.Id }, usuario);
        }

    }

    [ApiController]
    [Route("Cartelera")]
    public class CarteleraController : ControllerBase
    {
        private CarteleraService _serviceCartelera;
        private GeneroService _serviceGenero;
        private SalaService _serviceSala;
        public CarteleraController(CarteleraService serviceCartelera, GeneroService serviceGenero, SalaService serviceSala)
        {
            _serviceCartelera = serviceCartelera;
            _serviceGenero = serviceGenero;
            _serviceSala = serviceSala;
        }

        [HttpGet("PeliculasXSala")]
        public async Task<ActionResult> GetPeliculasXSala()
        {
            var peliculas = await _serviceCartelera.GetPeliculaXSala(0);
            if (peliculas == null || !peliculas.Any())
            {
                return NotFound($"No movies found for Sala with ID {0}.");
            }
            return Ok(peliculas);
        }
        [HttpGet("PeliculasXSala/{idPelicula}")]
        public async Task<ActionResult> GetPeliculasXSalaById(int idPelicula)
        {
            var peliculas = await _serviceCartelera.GetPeliculaXSala(idPelicula);
            if (peliculas == null || !peliculas.Any())
            {
                return NotFound($"No movies found for Sala with ID {idPelicula}.");
            }
            return Ok(peliculas);
        }

        [HttpPost("GetPeliculaByParam")]
        public async Task<ActionResult> GetPeliculaByParam(ListarPeliculasParam? param)
        {
            var peliculas = await _serviceCartelera.GetPeliculaByParam(param);
              if (peliculas == null || !peliculas.Any())
            {
                return NotFound($"No movies found for Sala.");
            }
            return Ok(peliculas);
        }

        [HttpGet("Generos")]
        public async Task<ActionResult> GetGeneros()
        {
            var generos = await _serviceGenero.GetGenerosAsync();
            if (generos == null || !generos.Any())
            {
                return NotFound($"No genders found.");
            }
            return Ok(generos);
        }
        [HttpGet("Salas")]
        public async Task<ActionResult> GetSalas()
        {
            var salas = await _serviceSala.GetSalasAsync();
            if (salas == null || !salas.Any())
            {
                return NotFound($"No salas found.");
            }
            return Ok(salas);
        }
    }

    [ApiController]
    [Route("Cine")]
    [Authorize]
    public class CineController : ControllerBase
    {
        private ReservaService _serviceReserva;
        public CineController(ReservaService serviceReserva)
        {
            _serviceReserva = serviceReserva;
        }

        [HttpGet("")]
        public IActionResult GetCine()
        {
            return Ok(new { Message = "Welcome to the Cine API!" });
        }

        [HttpGet("Reservas")]
        public async Task<ActionResult<List<Reserva>>> GetReservas()
        {
            var reservas = await _serviceReserva.GetReservasAsync();
            if (reservas == null || !reservas.Any())
            {
                return NotFound("No reservations found.");
            }

            return Ok(reservas);
        }
        [HttpPost("Reservas")]
        public async Task<ActionResult> AddReserva([FromBody] CreateReserva reserva)
        {
            if (reserva == null)
            {
                return BadRequest("Reservation data cannot be null.");
            }

            var reservaCompleta = await _serviceReserva.CreateReservaAsync(reserva);
            return Ok(reservaCompleta);
        }


    }
}