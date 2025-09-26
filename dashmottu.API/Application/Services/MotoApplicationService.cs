using dashmottu.API.Application.DTOs;
using dashmottu.API.Application.Interfaces;
using dashmottu.API.Application.Mappers;
using dashmottu.API.Domain.Entities;
using dashmottu.API.Domain.Interfaces;
using System.Net;

namespace dashmottu.API.Application.Services
{
    public class MotoApplicationService : IMotoApplicationService
    {

        private readonly IMotoRepository _repository;

        public MotoApplicationService(IMotoRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult<MotoResponse?>> Adicionar(MotoRequest moto)
        {
            try
            {
                var result = await _repository.Adicionar(moto.ToEntity());

                if (result == null) return OperationResult<MotoResponse?>.Failure("Erro ao adicionar moto", (int)HttpStatusCode.NotFound);

                return OperationResult<MotoResponse?>.Success(result.ToDto());
            }
            catch (Exception ex)
            {
                return OperationResult<MotoResponse?>.Failure(ex.Message);
            }
        }

        public async Task<OperationResult<MotoResponse?>> Atualizar(int id, MotoRequest moto)
        {
            try
            {
                var result = await _repository.Atualizar(id, moto.ToEntity());

                if (result == null) return OperationResult<MotoResponse?>.Failure("Nenhuma moto encontrada", (int)HttpStatusCode.NotFound);

                return OperationResult<MotoResponse?>.Success(result.ToDto());
            }
            catch (Exception ex)
            {

                return OperationResult<MotoResponse?>.Failure(ex.Message);
            }
        }

        public async Task<OperationResult<MotoResponse?>> Deletar(int id)
        {
            try
            {
                var result = await _repository.Deletar(id);

                if (result == null) return OperationResult<MotoResponse?>.Failure("Nenhuma moto encontrada", (int)HttpStatusCode.NotFound);

                return OperationResult<MotoResponse?>.Success(result.ToDto());
            }
            catch (Exception ex)
            {

                return OperationResult<MotoResponse?>.Failure(ex.Message);
            }
        }

        public async Task<OperationResult<MotoResponse?>> ObterPorId(int id)
        {
            try
            {
                var result = await _repository.ObterPorId(id);

                if (result == null) return OperationResult<MotoResponse?>.Failure("Nenhuma moto encontrada", (int)HttpStatusCode.NotFound);

                return OperationResult<MotoResponse?>.Success(result.ToDto());
            }
            catch (Exception ex)
            {

                return OperationResult<MotoResponse?>.Failure(ex.Message);
            }
        }

        public async Task<OperationResult<PageResultModel<IEnumerable<MotoResponse?>>>> ObterTodos(int deslocamento, int limite)
        {
            try
            {
                var result = await _repository.ObterTodos(deslocamento, limite);

                if (!result.Data.Any())
                    return OperationResult<PageResultModel<IEnumerable<MotoResponse?>>>.Failure("Não existe conteúdo para motos", (int)HttpStatusCode.NoContent);

                return OperationResult<PageResultModel<IEnumerable<MotoResponse?>>>.Success(result);
            }
            catch (Exception)
            {
                return OperationResult<PageResultModel<IEnumerable<MotoResponse?>>>.Failure("Erro ao obter motos.");
            }
        }
    }
}
