using dashmottu.API.Application.Interfaces;
using dashmottu.API.Application.Mappers;
using dashmottu.API.Domain.DTOs;
using dashmottu.API.Domain.Entities;
using dashmottu.API.Domain.Interfaces;
using System.Net;

namespace dashmottu.API.Application.Services
{
    public class AuthApplicationService : IAuthApplicationService
    {
        private readonly ILoginRepository _loginRepository;

        public AuthApplicationService(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        public async Task<OperationResult<LoginResponseDto?>> Adicionar(int idPatio, LoginDto entidade)
        {
            try
            {
                var result = await _loginRepository.Adicionar(idPatio, entidade.ToEntity());

                if (result is null) return OperationResult<LoginResponseDto?>.Failure("Erro ao adicionar");

                var response = new LoginResponseDto(result.ToDto(), true, idPatio, GenerateToken(idPatio));

                return OperationResult<LoginResponseDto?>.Success(response, (int)HttpStatusCode.Created);
            }
            //TODO: Implementar exception personalizada pra usuario existente
            // catch (UsuarioExistenteException ex) {}
            catch (Exception ex)
            {
                return OperationResult<LoginResponseDto?>.Failure(ex.Message);
            }
        }

        public async Task<OperationResult<LoginEntity?>> Deletar(int idPatio)
        {
            try
            {
                var result = await _loginRepository.Deletar(idPatio);

                if (result is null) return OperationResult<LoginEntity?>.Failure("Login não localizado");

                return OperationResult<LoginEntity?>.Success(result);
            }
            catch (Exception)
            {
                return OperationResult<LoginEntity?>.Failure("Erro ao deletar login");
            }
        }

        public async Task<OperationResult<LoginResponseDto?>> EditarLogin(int idPatio, LoginDto login)
        {
            try
            {
                var result = await _loginRepository.Atualizar(idPatio, login.ToEntity());

                if (result is null) return OperationResult<LoginResponseDto?>.Failure("Pátio não localizado");

                return OperationResult<LoginResponseDto?>.Success(new LoginResponseDto(result.ToDto(), true, idPatio, null));

            }
            catch (Exception)
            {
                return OperationResult<LoginResponseDto?>.Failure("Erro ao editar login");
            }
        }

        public async Task<OperationResult<LoginResponseDto?>> ValidarLogin(LoginDto entidade)
        {
            try
            {
                var result = await _loginRepository.VerificaUsuarioExistente(entidade.ToEntity());
                if (result is null)
                    return OperationResult<LoginResponseDto?>.Success(new LoginResponseDto(null, false, null, null), (int)HttpStatusCode.Unauthorized);

                return OperationResult<LoginResponseDto?>.Success(new LoginResponseDto(result.ToDto(), true, result.PatioId, GenerateToken(result.PatioId)));
            }
            catch (Exception)
            {
                return OperationResult<LoginResponseDto?>.Failure("Erro ao validar login");
            }
        }

        public async Task<OperationResult<LoginDto?>> ObterLoginPorId(int idPatio)
        {
            try
            {
                var result = await _loginRepository.ObterPorId(idPatio);
                if (result is null) return OperationResult<LoginDto?>.Failure("Login não localizado");
                return OperationResult<LoginDto?>.Success(result.ToDto());
            }
            catch (Exception)
            {
                return OperationResult<LoginDto?>.Failure("Erro ao obter login");
            }
        }
        private string GenerateToken(int id)
        {
            var rawToken = $"{id}-{DateTime.UtcNow.Ticks}-{Guid.NewGuid()}";
            var tokenBytes = System.Text.Encoding.UTF8.GetBytes(rawToken);
            return Convert.ToBase64String(tokenBytes);
        }
    }
}
