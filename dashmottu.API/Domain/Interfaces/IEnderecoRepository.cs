using dashmottu.API.Domain.Entities;

namespace dashmottu.API.Domain.Interfaces
{
    public interface IEnderecoRepository
    {
        EnderecoEntity? Adicionar(EnderecoEntity endereco);
    }
}