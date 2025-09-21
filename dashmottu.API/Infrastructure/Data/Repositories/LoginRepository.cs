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

        public async Task<LoginEntity?> Adicionar(int idPatio, LoginEntity login)
        {
            var patio = await _context.Patio.FindAsync(idPatio);

            if (patio is not null)
            {
                login.PatioId = idPatio;

                _context.Login.Add(login);
                await _context.SaveChangesAsync();  

                return login;
            }
            return null;
        }

        public async Task<LoginEntity?> Atualizar(int idPatio, LoginEntity login)
        {
            var result = await _context.Patio
                .Include(x => x.Login)
                .FirstOrDefaultAsync(x => x.Id == idPatio);

            if (result is not null)
            {
                //TODO: Verificar se o usuario ou senha é igual ao que ja existe no banco
                
                if (result.Login is null)
                {
                    result.Login = new LoginEntity
                    {
                        Id = login.Id,
                        Usuario = login.Usuario,
                        Senha = login.Senha
                    };
                } else 
                    result.Login = login;

                _context.Login.Update(login);
                await _context.SaveChangesAsync();

                return result.Login;
            }
            return null;
        }

        public async Task<LoginEntity?> Deletar(int idPatio, LoginEntity? login)
        {
            var result = await _context.Patio
                .Include(x => x.Login)
                .FirstOrDefaultAsync(x => x.Id == idPatio);

            if (result is not null || result.Login is not null)
            {
                _context.Login.Remove(login);
                _context.SaveChanges();

                return result.Login;
            }

            return null;
        }

        public async Task<LoginEntity?> ObterPorId(int id)
        {
            return await _context.Login.FindAsync(id);
        }

        public async Task<LoginEntity?> VerificaUsuarioExistente(LoginEntity login)
        {
            return await _context.Login.FirstOrDefaultAsync(l => l.Usuario == login.Usuario);
        }
    }
}
