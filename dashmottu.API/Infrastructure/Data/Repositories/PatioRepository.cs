using dashmottu.API.Application.Mappers;
using dashmottu.API.Domain.DTOs;
using dashmottu.API.Domain.Entities;
using dashmottu.API.Domain.Interfaces;
using dashmottu.API.Infrastructure.Data.AppData;
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

        public async Task<PatioEntity?> ObterEntityPorId(int id)
        {
            return await _context.Patio
                .Include(p => p.Endereco)
                .Include(p => p.Login)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<PatioResponse?> ObterPorId(int id)
        {
            return await _context.Patio
                .Include(p => p.Endereco)
                .Where(p => p.Id == id)
                .Select(p => new PatioResponse(
                    p.Id,
                    p.UrlImgPlanta,
                    p.Endereco.ToDto()
                ))
                .FirstOrDefaultAsync();
        }

        public async Task<PageResultModel<IEnumerable<PatioResponse?>>> ObterTodos(int deslocamento, int limite)
        {
            var total = await _context.Patio.CountAsync();
            var patios = await _context.Patio
                .Include(p => p.Endereco)
                .OrderBy(p => p.Id)
                .Select(p => new PatioResponse(
                    p.Id,
                    p.UrlImgPlanta,
                    p.Endereco.ToDto()
                ))
                .Skip(deslocamento)
                .Take(limite)
                .ToListAsync();

            return new PageResultModel<IEnumerable<PatioResponse?>>
            {
                Data = patios,
                Deslocamento = deslocamento,
                Limite = limite,
                Total = total
            };
        }
    }
}
