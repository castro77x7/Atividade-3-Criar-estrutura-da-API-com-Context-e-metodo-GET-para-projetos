using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Exo.WebApi.Models;
using Exo.WebApi.Repositories;
using System;

namespace Exo.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioRepository _usuarioRepository;

        public UsuariosController(UsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository ?? throw new ArgumentNullException(nameof(usuarioRepository));
        }

        // GET: api/usuarios
        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(_usuarioRepository.Listar());
        }

        // POST: api/usuarios
        [HttpPost]
        public IActionResult Cadastrar([FromBody] Usuario usuario)
        {
            if (usuario == null)
            {
                return BadRequest("Dados inválidos para cadastro.");
            }

            _usuarioRepository.Cadastrar(usuario);
            return StatusCode(201);
        }

        // GET: api/usuarios/{id}
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            Usuario usuario = _usuarioRepository.BuscaPorId(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        // PUT: api/usuarios/{id}
        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] Usuario usuario)
        {
            if (usuario == null)
            {
                return BadRequest("Dados inválidos para atualização.");
            }

            _usuarioRepository.Atualizar(id, usuario);
            return StatusCode(204);
        }

        // DELETE: api/usuarios/{id}
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                _usuarioRepository.Deletar(id);
                return StatusCode(204);
            }
            catch (Exception e)
            {
                // Log the exception or handle it appropriately
                return BadRequest("Erro ao deletar usuário.");
            }
        }
    }
}
