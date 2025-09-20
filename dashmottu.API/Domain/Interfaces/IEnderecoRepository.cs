using dashmottu.API.Domain.Entities;

namespace dashmottu.API.Domain.Interfaces
{
    public interface IEnderecoRepository
    {
        Task<EnderecoEntity?> Adicionar(int Id, EnderecoEntity endereco);
        Task<EnderecoEntity?> Atualizar(EnderecoEntity endereco);
        Task<EnderecoEntity?> ObterPorId(int id);
        void Deletar(EnderecoEntity endereco);
    }
}