using dashmottu.API.Application.DTOs;
using dashmottu.API.Application.Mappers;
using dashmottu.API.Domain.DTOs;
using dashmottu.API.Domain.Entities;
using dashmottu.API.Domain.Interfaces;
using dashmottu.API.Infrastructure.Data.AppData;
using Microsoft.EntityFrameworkCore;

namespace dashmottu.API.Infrastructure.Data.Repositories
{
    public class MotoRespository : IMotoRepository
    {
        private readonly ApplicationContext _context;
        public MotoRespository(ApplicationContext context) 
        {
            _context = context;
        }

        public async Task<MotoEntity?> Adicionar(MotoEntity moto)
        {
            _context.Moto.Add(moto);
            await _context.SaveChangesAsync();

            return moto;
        }

        public async Task<MotoEntity?> Atualizar(int id, MotoEntity moto)
        {
            var result = await _context.Moto.FindAsync(id);
            if (result is not null)
            {
                result.CodTag = moto.CodTag;
                result.Modelo = moto.Modelo;
                result.Placa = moto.Placa;
                result.Status = moto.Status;

                _context.Moto.Update(result);
                await _context.SaveChangesAsync();
            }
            return result;
        }

        public async Task<MotoEntity?> Deletar(int id)
        {
            var result = await _context.Moto.FindAsync(id);

            if (result is not null)
            {
                _context.Moto.Remove(result);
                await _context.SaveChangesAsync();
            }
            return result;

        }

        public async Task<MotoEntity?> ObterPorId(int id)
        {
            return await _context.Moto
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<PageResultModel<IEnumerable<MotoResponse?>>> ObterTodos(int deslocamento, int limite)
        {
            var total = await _context.Moto.CountAsync();
            var patios = await _context.Moto
                .OrderBy(m => m.Id)
                .Select(m => new MotoResponse(m.Id, m.CodTag, m.Modelo, m.Placa, m.Status))
                .Skip(deslocamento)
                .Take(limite)
                .ToListAsync();

            return new PageResultModel<IEnumerable<MotoResponse?>>
            {
                Data = patios,
                Deslocamento = deslocamento,
                Limite = limite,
                Total = total
            };
        }
    }
}
