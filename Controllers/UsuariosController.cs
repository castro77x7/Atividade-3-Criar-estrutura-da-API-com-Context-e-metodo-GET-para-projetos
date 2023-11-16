using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Exo.WebApi.Models;
using Exo.WebApi.Repositories;
using System;
using Microsoft.IdentityModel.Tokens; 
using System.IdentityModel.Tokens.Jwt; 
using System.Security.Claims;

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
        //[HttpPost]
        //public IActionResult Cadastrar([FromBody] Usuario usuario)
        //{
            //if (usuario == null)
            //{
                //return BadRequest("Dados inválidos para cadastro.");
            //}

            //_usuarioRepository.Cadastrar(usuario);
            //return StatusCode(201);
       // }

       public IActionResult Post(Usuario usuario)
{
    // Tenta autenticar o usuário
    Usuario usuarioBuscado = _usuarioRepository.Login(usuario.Email, usuario.Senha);

    if (usuarioBuscado == null)
    {
        return NotFound("E-mail ou senha inválidos!");
    }

    // Se o usuário for encontrado, segue a criação do token.

    // Define os dados que serão fornecidos no token - Payload.
    var claims = new[]
    {
        // Armazena na claim o e-mail do usuário autenticado.
        new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.Email),
        // Armazena na claim o id do usuário autenticado.
        new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.Id.ToString()),
    };

    // Define a chave de acesso ao token.
    var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("exoapi-chaveautenticacao"));

    // Define as credenciais do token.
    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    // Gera o token.
    var token = new JwtSecurityToken(
        issuer: "exoapi.webapi",      // Emissor do token.
        audience: "exoapi.webapi",    // Destinatário do token.
        claims: claims,               // Dados definidos acima.
        expires: DateTime.Now.AddMinutes(30),    // Tempo de expiração.
        signingCredentials: creds      // Credenciais do token.
    );

    // Retorna OK com o token.
    return Ok(new
    {
        token = new JwtSecurityTokenHandler().WriteToken(token)
    });
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
        [Authorize]
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
        [Authorize]
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
