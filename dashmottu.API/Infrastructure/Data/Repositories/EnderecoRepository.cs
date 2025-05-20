using dashmottu.API.Domain.Entities;
using dashmottu.API.Domain.Interfaces;
using dashmottu.API.Infrastructure.Data.AppData;

namespace dashmottu.API.Infrastructure.Data.Repositories
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly ApplicationContext _context;

        public EnderecoRepository(ApplicationContext context)
        {
            _context = context;
        }

        public EnderecoEntity? Adicionar(EnderecoEntity endereco)
        {
            if (endereco != null)
            {
                _context.Endereco.Add(endereco);
                _context.SaveChanges();
            }
            return endereco;
        }

        public EnderecoEntity? Atualizar(EnderecoEntity endereco)
        {
            if (endereco != null)
            {
                _context.Endereco.Update(endereco);
                _context.SaveChanges();
            }
            return endereco;
        }

        public EnderecoEntity? ObterPorId(int id)
        {
            return _context.Endereco.Find(id);
        }
    }
}
