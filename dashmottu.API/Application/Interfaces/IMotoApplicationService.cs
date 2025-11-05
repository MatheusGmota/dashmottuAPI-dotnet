using dashmottu.API.Application.DTOs;
using dashmottu.API.Domain.Entities;

namespace dashmottu.API.Application.Interfaces
{
    public interface IMotoApplicationService
    {
        Task<OperationResult<MotoResponse?>> Adicionar(MotoRequest moto);

        Task<OperationResult<MotoResponse?>> Atualizar(int id, MotoRequest moto);

        //Task<MotoResponse?> AtualizarLocalizacao(int id, Localizacao localizacao);

        Task<OperationResult<MotoWithXAndYResponse?>> AdicionarMotoNoPatio(int idPatio, MotoRequest moto);

        Task<OperationResult<PageResultModel<IEnumerable<MotoResponse?>>>> ObterTodos(int deslocamento, int limite);

        Task<OperationResult<MotoResponse?>> ObterPorId(int id);

        Task<OperationResult<MotoResponse?>> Deletar(int id);
    }
}
