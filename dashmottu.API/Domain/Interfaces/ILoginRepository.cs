using dashmottu.API.Domain.DTOs;
using dashmottu.API.Domain.Entities;

namespace dashmottu.API.Domain.Interfaces
{
    public interface ILoginRepository
    {
        Task<LoginEntity?> Adicionar(int idPatio, LoginEntity login);
        Task<LoginEntity?> Atualizar(int idPatio, LoginEntity login);
        Task<LoginEntity?> Deletar(int idPatio);
        Task<LoginEntity?> ObterPorId(int id);
        Task<LoginEntity?> VerificaUsuarioExistente(LoginEntity login);

    }
}