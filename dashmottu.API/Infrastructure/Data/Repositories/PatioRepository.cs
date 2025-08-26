using dashmottu.API.Infrastructure.Data.AppData;
using dashmottu.API.Domain.Entities;
using dashmottu.API.Domain.Interfaces;
using dashmottu.API.Domain.DTOs;
using Microsoft.EntityFrameworkCore;

namespace dashmottu.API.Infrastructure.Data.Repositories
{
    public class PatioRepository : IPatioRepository
    {
        private readonly ApplicationContext _context;

        public PatioRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<PatioEntity?> Adicionar(PatioEntity patio)
        {
            if (patio != null)
            {
                _context.Patio.Add(patio);
                await _context.SaveChangesAsync();
                return patio;
            }
            return null;
        }

        public async Task<PatioEntity?> Atualizar(PatioEntity patio)
        {
            _context.Patio.Update(patio);
            _context.SaveChanges();

            return patio;
        }

        public void Deletar(PatioEntity patio)
        {
            _context.Patio.Remove(patio);
            _context.SaveChanges(); 
        }

        public async Task<PatioEntity?> ObterPorId(int id)
        {
            return await _context.Patio.FindAsync(id);
        }

        public async Task<IEnumerable<PatioEntity>?> ObterTodos()
        {
            return await _context.Patio.OrderBy(o => o.Id).ToListAsync();
        }
    }
}
