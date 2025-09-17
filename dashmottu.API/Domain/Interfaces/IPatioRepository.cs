using dashmottu.API.Domain.DTOs;
using dashmottu.API.Domain.Entities;

namespace dashmottu.API.Domain.Interfaces
{
    public interface IPatioRepository
    {
        Task<PatioEntity?> Adicionar(PatioEntity patio);

        Task<PatioEntity?> Atualizar(PatioEntity patio);

        Task<IEnumerable<PatioResponse>?> ObterTodos();

        Task<PatioResponse?> ObterPorId(int id);

        Task<PatioEntity?> ObterEntityPorId(int id);

        void Deletar(PatioEntity patio);
    }
}