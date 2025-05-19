using dashmottu.API.Domain.Entities;

namespace dashmottu.API.Domain.Interfaces
{
    public interface ILoginRepository
    {
        LoginEntity? Adicionar(LoginEntity login);
    }
}