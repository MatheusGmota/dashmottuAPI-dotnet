using dashmottu.API.Application.DTOs;
using dashmottu.API.Domain.Entities;

namespace dashmottu.API.Application.Interfaces
{
    public interface IAuthApplicationService
    {
        Task<OperationResult<LoginDto?>> ObterLoginPorId(int idPatio);
        Task<OperationResult<LoginResponseDto?>> Adicionar(int? idPatio, LoginRequestDto entidade);
        Task<OperationResult<LoginResponseDto?>> ValidarLogin(LoginDto login);
        Task<OperationResult<LoginResponseDto?>> EditarLogin(int idPatio, LoginDto login);
        Task<OperationResult<LoginEntity?>> Deletar(int idPatio);
    }
}