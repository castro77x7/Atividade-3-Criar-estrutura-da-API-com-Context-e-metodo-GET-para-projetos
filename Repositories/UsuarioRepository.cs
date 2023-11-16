using Exo.WebApi.Contexts;
using Exo.WebApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace Exo.WebApi.Repositories
{
    public class UsuarioRepository
    {
        private readonly ExoContext _context;

        public UsuarioRepository(ExoContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Realiza o login do usuário com base no email e senha.
        /// </summary>
        public Usuario Login(string email, string senha)
        {
            return _context.Usuarios.FirstOrDefault(u => u.Email == email && u.Senha == senha);
        }

        /// <summary>
        /// Retorna uma lista de todos os usuários.
        /// </summary>
        public List<Usuario> Listar()
        {
            return _context.Usuarios.ToList();
        }

        /// <summary>
        /// Cadastra um novo usuário.
        /// </summary>
        public void Cadastrar(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }

        /// <summary>
        /// Busca um usuário pelo ID.
        /// </summary>
        public Usuario BuscaPorId(int id)
        {
            return _context.Usuarios.Find(id);
        }

        /// <summary>
        /// Atualiza as informações de um usuário.
        /// </summary>
        public void Atualizar(int id, Usuario usuario)
        {
            Usuario usuarioBuscado = _context.Usuarios.Find(id);

            if (usuarioBuscado != null)
            {
                usuarioBuscado.Email = usuario.Email;
                usuarioBuscado.Senha = usuario.Senha;
            }

            _context.Usuarios.Update(usuarioBuscado);
            _context.SaveChanges();
        }

        /// <summary>
        /// Deleta um usuário pelo ID.
        /// </summary>
        public void Deletar(int id)
        {
            Usuario usuarioBuscado = _context.Usuarios.Find(id);

            if (usuarioBuscado != null)
            {
                _context.Usuarios.Remove(usuarioBuscado);
                _context.SaveChanges();
            }
        }
    }
}
