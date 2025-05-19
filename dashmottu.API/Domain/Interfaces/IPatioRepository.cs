using dashmottu.API.Domain.Entities;

namespace dashmottu.API.Domain.Interfaces
{
    public interface IPatioRepository
    {
        PatioEntity? Adicionar (PatioEntity patio);
    }
}