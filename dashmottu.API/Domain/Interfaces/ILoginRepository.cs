using dashmottu.API.Domain.DTOs;
using dashmottu.API.Domain.Entities;

namespace dashmottu.API.Domain.Interfaces
{
    public interface ILoginRepository
    {
        LoginEntity? Adicionar(LoginEntity login);
        LoginEntity? Atualizar(LoginEntity login);
        LoginEntity? ObterPorId(int id);

    }
}