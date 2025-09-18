using dashmottu.API.Domain.DTOs;
using dashmottu.API.Domain.Entities;
using dashmottu.API.Domain.Model;

namespace dashmottu.API.Domain.Interfaces
{
    public interface IPatioRepository
    {
        Task<PatioEntity?> Adicionar(PatioEntity patio);

        Task<PatioEntity?> Atualizar(PatioEntity patio);

        Task<PaginacaoModel<IEnumerable<PatioResponse?>>> ObterTodos(int deslocamento, int limite);

        Task<PatioResponse?> ObterPorId(int id);

        Task<PatioEntity?> ObterEntityPorId(int id);

        void Deletar(PatioEntity patio);
    }
}