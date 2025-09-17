using dashmottu.API.Application.Interfaces;
using dashmottu.API.Domain.DTOs;
using dashmottu.API.Domain.Interfaces;
using dashmottu.API.Mappers;

namespace dashmottu.API.Application.Services
{
    public class AuthApplicationService : IAuthApplicationService
    {
        private readonly ILoginRepository _loginRepository;

        public AuthApplicationService(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        public async Task<LoginResponseDto> Adicionar(int idPatio, LoginDto entidade)
        {
            var login = entidade.ToEntity();

            // Verificar se já existe um usuário com o mesmo nome
            var usuarioExistente = await _loginRepository.VerificaUsuarioExistente(login);

            if (usuarioExistente == null) {
                login.PatioId = idPatio;
                await _loginRepository.Adicionar(login);
                return new LoginResponseDto(
                    true,
                    idPatio,
                    new GeneratorToken().GenerateToken(idPatio)
                );
            }

            return new LoginResponseDto(false, null, null);
        }

        public async Task<LoginResponseDto> ValidarLogin(LoginDto login)
        {
            var loginEntity = login.ToEntity();
            var usuarioExistente = await _loginRepository.VerificaUsuarioExistente(loginEntity);
            if (usuarioExistente != null && usuarioExistente.Senha == loginEntity.Senha)
            {
                return new LoginResponseDto(
                    true,
                    usuarioExistente.PatioId,
                    new GeneratorToken().GenerateToken(usuarioExistente.PatioId)
                );
            }
            return new LoginResponseDto(false, null, null);
        }
    }
}
