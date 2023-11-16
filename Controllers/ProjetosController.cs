using Exo.WebApi.Models;
using Exo.WebApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Exo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjetosController : ControllerBase
    {
        private readonly ProjetoRepository projetoRepository;

        public ProjetosController(ProjetoRepository projetoRepository)
        {
            this.projetoRepository = projetoRepository ?? throw new ArgumentNullException(nameof(projetoRepository));
        }

        [HttpGet]
        public IActionResult Listar()
        {
            var projetos = projetoRepository.Listar;
            return Ok(projetos);
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] Projeto projeto)
        {
            projetoRepository.Cadastrar(projeto);
            return StatusCode(201);
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            var projeto = projetoRepository.BuscarporId(id);

            if (projeto == null)
            {
                return NotFound();
            }

            return Ok(projeto);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] Projeto projeto)
        {
            projetoRepository.Atualizar(id, projeto);
            return StatusCode(204);
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                projetoRepository.Deletar(id);
                return StatusCode(204);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
