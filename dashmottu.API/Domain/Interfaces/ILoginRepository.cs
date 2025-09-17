using dashmottu.API.Domain.DTOs;
using dashmottu.API.Domain.Entities;

namespace dashmottu.API.Domain.Interfaces
{
    public interface ILoginRepository
    {
        Task<LoginEntity?> Adicionar(LoginEntity login);
        Task<LoginEntity?> Atualizar(LoginEntity login);
        void Deletar(LoginEntity? login);
        Task<LoginEntity?> ObterPorId(int id);
        Task<LoginEntity?> VerificaUsuarioExistente(LoginEntity login);

    }
}