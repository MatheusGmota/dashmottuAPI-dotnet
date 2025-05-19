using dashmottu.API.Domain.Entities;
using dashmottu.API.Domain.Interfaces;
using dashmottu.API.Infrastructure.Data.AppData;

namespace dashmottu.API.Infrastructure.Data.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly ApplicationContext _context;

        public LoginRepository(ApplicationContext context)
        {
            _context = context;
        }

        public LoginEntity? Adicionar(LoginEntity login)
        {
            _context.Login.Add(login);
            _context.SaveChanges();  

            return login;
        }
    }
}
