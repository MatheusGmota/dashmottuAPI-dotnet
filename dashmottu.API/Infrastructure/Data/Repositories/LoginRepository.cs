using dashmottu.API.Application;
using dashmottu.API.Domain.DTOs;
using dashmottu.API.Domain.Entities;
using dashmottu.API.Domain.Interfaces;
using dashmottu.API.Infrastructure.Data.AppData;
using Microsoft.EntityFrameworkCore;

namespace dashmottu.API.Infrastructure.Data.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly ApplicationContext _context;

        public LoginRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<LoginEntity?> Adicionar(LoginEntity login)
        {
            _context.Login.Add(login);
            await _context.SaveChangesAsync();  

            return login;
        }

        public async Task<LoginEntity?> Atualizar(LoginEntity login)
        {
            _context.Login.Update(login);
            await _context.SaveChangesAsync();

            return login;
        }

        public void Deletar(LoginEntity? login)
        {
            _context.Login.Remove(login);
            _context.SaveChanges();
        }

        public async Task<LoginEntity?> ObterPorId(int id)
        {
            return await _context.Login.FindAsync(id);
        }

        public async Task<LoginResponseDto> ValidarLogin(LoginDto login)
        {
            var loginEntity = await _context.Login.Include(l => l.Patio).FirstOrDefaultAsync(l => l.Usuario == login.Usuario);

            if (loginEntity == null || loginEntity.Senha != login.Senha)
                return new LoginResponseDto(false, null, null);

            return new LoginResponseDto(
                true,
                loginEntity.Patio?.Id,
                new GeneratorToken().GenerateToken(loginEntity.Id)
            );
        }

        public LoginEntity? VerificaUsuarioExistente(LoginEntity login)
        {
            return _context.Login.FirstOrDefault(x => x.Usuario == login.Usuario);
        }
    }
}
