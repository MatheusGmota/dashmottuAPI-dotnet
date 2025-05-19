using dashmottu.API.Infrastructure.Data.AppData;
using dashmottu.API.Domain.Entities;
using dashmottu.API.Domain.Interfaces;

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
    }
}
