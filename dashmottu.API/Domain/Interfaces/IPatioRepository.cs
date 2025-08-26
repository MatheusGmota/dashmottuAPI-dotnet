using dashmottu.API.Domain.DTOs;
using dashmottu.API.Domain.Entities;

namespace dashmottu.API.Domain.Interfaces
{
    public interface IPatioRepository
    {
        Task<PatioEntity?> Adicionar(PatioEntity patio);

        Task<PatioEntity?> Atualizar(PatioEntity patio);

        Task<IEnumerable<PatioEntity>?> ObterTodos();

        Task<PatioEntity?> ObterPorId(int id);

        void Deletar(PatioEntity patio);
    }
}