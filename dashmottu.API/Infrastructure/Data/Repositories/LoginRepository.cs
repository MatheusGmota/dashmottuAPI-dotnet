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
            var patio = await _context.Patio.FirstOrDefaultAsync(x => x.Id == idPatio);

            if (patio is not null)
            {
                login.PatioId = idPatio;

                //Verificar se ja existe um login para o patio
                var existingLogin = await _context.Login.FirstOrDefaultAsync(l => l.Usuario == login.Usuario);
                if (existingLogin is not null)
                    //Lancar exception personalizada de usuario existente
                    return null;

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
                //Verifica se o usuario ja existe em outro login
                var existingLogin = await _context.Login.FirstOrDefaultAsync(l => l.Usuario == login.Usuario);
                if (existingLogin is not null)
                    //Lancar exception personalizada de usuario existente
                    return null;

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

        public async Task<LoginEntity?> Deletar(int idPatio)
        {
            var result = await _context.Login.FirstOrDefaultAsync(x => x.PatioId == idPatio);

            if (result is null) return null;

            _context.Login.Remove(result);
            await _context.SaveChangesAsync();

            return result;
        }

        public async Task<LoginEntity?> ObterPorId(int idPatio)
        {
            return await _context.Login.FirstOrDefaultAsync(x => x.PatioId == idPatio);
        }

        public async Task<LoginEntity?> VerificaUsuarioExistente(LoginEntity login)
        {
            var result = await _context.Login.FirstOrDefaultAsync(l => l.Usuario == login.Usuario);
            if (result is not null)
            {
                if (result.Senha == login.Senha)
                    return result;
            };
            return null;
        }
    }
}
