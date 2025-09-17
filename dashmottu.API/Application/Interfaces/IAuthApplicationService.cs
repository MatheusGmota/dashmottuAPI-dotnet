using dashmottu.API.Domain.DTOs;

namespace dashmottu.API.Application.Interfaces
{
    public interface IAuthApplicationService
    {
        Task<LoginResponseDto> Adicionar(int idPatio, LoginDto entidade);
        Task<LoginResponseDto> ValidarLogin(LoginDto login);
    }
}