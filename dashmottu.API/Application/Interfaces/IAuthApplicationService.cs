using dashmottu.API.Domain.DTOs;
using dashmottu.API.Domain.Entities;

namespace dashmottu.API.Application.Interfaces
{
    public interface IAuthApplicationService
    {
        Task<OperationResult<LoginResponseDto?>> Adicionar(int idPatio, LoginDto entidade);
        Task<OperationResult<LoginResponseDto?>> ValidarLogin(LoginDto login);
        Task<OperationResult<LoginResponseDto?>> EditarLogin(int idPatio, LoginDto login);
        Task<OperationResult<LoginResponseDto?>> Deletar(int idPatio, LoginDto login);
    }
}