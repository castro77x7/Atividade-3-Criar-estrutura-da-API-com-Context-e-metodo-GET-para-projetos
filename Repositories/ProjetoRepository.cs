using Exo.WebApi.Contexts;
using Exo.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exo.WebApi.Repositories
{
    public class ProjetoRepository
    {
        private readonly ExoContext context;

        public ProjetoRepository(ExoContext context)
        {
            this.context = context;
        }

        public List<Projeto> Listar => context.Projetos.ToList();
    }
}
