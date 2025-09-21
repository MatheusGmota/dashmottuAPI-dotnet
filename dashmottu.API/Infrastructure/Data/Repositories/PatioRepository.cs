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
            _context.Add(patio);
            await _context.SaveChangesAsync();
            
            return patio;
        }

        public async Task<PatioEntity?> Atualizar(int id, PatioEntity patio)
        {
            var result = await _context.Patio
                .Include(x => x.Endereco)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (result is not null)
            {   
                result.UrlImgPlanta = patio.UrlImgPlanta;
             
                if (result.Endereco is null)
                {
                    result.Endereco = new EnderecoEntity
                    {
                        Id = patio.Endereco.Id,
                        Cep = patio.Endereco.Cep,
                        Logradouro = patio.Endereco.Logradouro,
                        Numero = patio.Endereco.Numero,
                        Bairro = patio.Endereco.Bairro,
                        Cidade = patio.Endereco.Cidade,
                        Estado = patio.Endereco.Estado,
                    };
                }
                else 
                    result.Endereco = patio.Endereco;

                _context.Patio.Update(result);
                _context.Endereco.Update(result.Endereco);
                _context.SaveChanges();
                
                return result;
            }

            return null;
        }

        public async Task<PatioEntity?> Deletar(int id)
        {
            var result = await _context.Patio.FindAsync(id);

            if (result is not null)
            {
                _context.Patio.Remove(result);
                _context.SaveChanges();

                return result;
            }

            return null;
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
