using dashmottu.API.Application.Interfaces;
using dashmottu.API.Application.Mappers;
using dashmottu.API.Domain.DTOs;
using dashmottu.API.Domain.Entities;
using dashmottu.API.Domain.Interfaces;
using System.Net;

namespace dashmottu.API.Application.Services
{
    public class PatioApplicationService : IPatioApplicationService
    {
        private readonly IPatioRepository _repository;

        private readonly IEnderecoRepository _enderecoRepository;

        public PatioApplicationService(IPatioRepository patioRepository, IEnderecoRepository enderecoRepository)
        {
            _repository = patioRepository;
            _enderecoRepository = enderecoRepository;
        }

        public async Task<OperationResult<PatioResponse?>> AdicionarPatio(PatioRequest entidade)
        {
            try
            {
                var result = await _repository.Adicionar(entidade.ToEntity());

                var endereco = await _enderecoRepository.Adicionar(result.Id, entidade.Endereco.ToEntity());

                result.Endereco = endereco;

                return OperationResult<PatioResponse?>.Success(result.ToResponse(), (int)HttpStatusCode.Created);
            }
            //TODO: Criar uma exceção personalizada para erro ao persistir patio e endereco
            catch (Exception)
            {
                return OperationResult<PatioResponse?>.Failure("Erro ao cadastrar pátio.");
            }
        }

        public async Task<OperationResult<PatioEntity?>> DeletarPatio(int id)
        {
            try
            {
                var result = await _repository.Deletar(id);

                if (result is null) return OperationResult<PatioEntity?>.Failure("Pátio não encontrado", (int)HttpStatusCode.NoContent);

                return OperationResult<PatioEntity?>.Success(result);
            }
            catch (Exception)
            {
                return OperationResult<PatioEntity?>.Failure("Erro ao deletar pátio.");
            }
        }

        public async Task<OperationResult<PatioResponse?>> EditarPatio(int id, PatioRequest entidade)
        {
            try
            {
                var result = await _repository.Atualizar(id, entidade.ToEntity());

                if (result is null) return OperationResult<PatioResponse?>.Failure("Pátio não encontrado", (int)HttpStatusCode.NoContent);

                return OperationResult<PatioResponse?>.Success(result.ToResponse());
            }
            catch (Exception ex)
            {
                return OperationResult<PatioResponse?>.Failure("Erro ao editar pátio.");
            }
        }

        public async Task<OperationResult<PatioResponse?>> ObterPatioPorId(int id)
        {
            try
            {
                var result = await _repository.ObterPorId(id);

                if (result is null) return OperationResult<PatioResponse?>.Failure("Pátio não encontrado", (int)HttpStatusCode.NoContent);
                    
                return OperationResult<PatioResponse?>.Success(result);
            }
            catch (Exception)
            {
                return OperationResult<PatioResponse?>.Failure("Erro ao obter pátio.");
            }
        }

        public async Task<OperationResult<PageResultModel<IEnumerable<PatioResponse?>>>> ObterTodosPatios(int deslocamento, int limite)
        {
            try
            {
                var result = await _repository.ObterTodos(deslocamento, limite);

                if (!result.Data.Any())
                    return OperationResult<PageResultModel<IEnumerable<PatioResponse?>>>.Failure("Não existe conteúdo para pátios", (int)HttpStatusCode.NoContent);

                return OperationResult<PageResultModel<IEnumerable<PatioResponse?>>>.Success(result);
            }
            catch (Exception)
            {
                return OperationResult<PageResultModel<IEnumerable<PatioResponse?>>>.Failure("Erro ao obter pátios.");
            }
        }
    }
}
