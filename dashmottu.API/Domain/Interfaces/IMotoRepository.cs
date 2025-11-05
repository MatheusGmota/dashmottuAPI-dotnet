using dashmottu.API.Application.DTOs;
using dashmottu.API.Domain.Entities;

namespace dashmottu.API.Domain.Interfaces
{
    public interface IMotoRepository
    {
        Task<MotoEntity?> Adicionar(MotoEntity moto);

        Task<MotoEntity?> Atualizar(int id, MotoEntity moto);

        //Task<MotoEntity?> AtualizarLocalizacao(int id, Localizacao localizacao);

        Task<MotoEntity?> AdicionarMotoNoPatio(int idPatio, MotoEntity entidade);

        Task<PageResultModel<IEnumerable<MotoResponse?>>> ObterTodos(int deslocamento, int limite);

        Task<MotoEntity?> ObterPorId(int id);

        Task<MotoEntity?> Deletar(int id);
    }
}
