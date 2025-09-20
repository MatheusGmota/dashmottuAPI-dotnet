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

        public async Task<EnderecoEntity?> Adicionar(int IdPatio, EnderecoEntity endereco)
        {
            endereco.PatioId = IdPatio;
            
            _context.Endereco.Add(endereco);
            await _context.SaveChangesAsync();
            
            return endereco;
        }

        public async Task<EnderecoEntity?> Atualizar(EnderecoEntity endereco)
        {
            if (endereco != null)
            {
                _context.Endereco.Update(endereco);
                await _context.SaveChangesAsync();
            }
            return endereco;
        }

        public void Deletar(EnderecoEntity endereco)
        {
            _context.Endereco.Remove(endereco);
            _context.SaveChanges();
        }

        public async Task<EnderecoEntity?> ObterPorId(int id)
        {
            return await _context.Endereco.FindAsync(id);
        }
    }
}
