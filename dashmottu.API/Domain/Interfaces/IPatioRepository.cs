using dashmottu.API.Domain.DTOs;
using dashmottu.API.Domain.Entities;

namespace dashmottu.API.Domain.Interfaces
{
    public interface IPatioRepository
    {
        PatioEntity? Adicionar(PatioEntity patio);

        PatioEntity? Atualizar(PatioEntity patio);

        IEnumerable<PatioEntity>? ObterTodos();

        PatioEntity? ObterPorId(int id);

        void Deletar(PatioEntity patio);
    }
}