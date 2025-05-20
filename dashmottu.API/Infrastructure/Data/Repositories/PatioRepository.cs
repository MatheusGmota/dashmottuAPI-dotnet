using dashmottu.API.Infrastructure.Data.AppData;
using dashmottu.API.Domain.Entities;
using dashmottu.API.Domain.Interfaces;
using dashmottu.API.Domain.DTOs;

namespace dashmottu.API.Infrastructure.Data.Repositories
{
    public class PatioRepository : IPatioRepository
    {
        private readonly ApplicationContext _context;

        public PatioRepository(ApplicationContext context)
        {
            _context = context;
        }

        public PatioEntity? Adicionar(PatioEntity patio)
        {
            if (patio != null)
            {
                _context.Patio.Add(patio);
                _context.SaveChanges();
            }
            return patio;
        }

        public PatioEntity? Atualizar(PatioEntity patio)
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

        public PatioEntity? ObterPorId(int id)
        {
            return _context.Patio.Find(id);
        }

        public IEnumerable<PatioEntity>? ObterTodos()
        {
            return _context.Patio.OrderBy(o => o.Id).ToList();
        }
    }
}
