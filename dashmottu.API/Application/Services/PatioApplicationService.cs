using dashmottu.API.Application.Interfaces;
using dashmottu.API.Domain.DTOs;
using dashmottu.API.Domain.Entities;
using dashmottu.API.Domain.Interfaces;
using dashmottu.API.Mappers;
using System.IO;

namespace dashmottu.API.Application.Services
{
    public class PatioApplicationService : IPatioApplicationService
    {
        private readonly IPatioRepository _repository;

        private readonly ILoginRepository _loginRepository;

        private readonly IEnderecoRepository _enderecoRepository;

        public PatioApplicationService(IPatioRepository patioRepository, IEnderecoRepository enderecoRepository, ILoginRepository loginRepository)
        {
            _repository = patioRepository;
            _loginRepository = loginRepository;
            _enderecoRepository = enderecoRepository;
        }

        public async Task<PatioResponse?> AdicionarPatio(PatioRequest entidade)
        {
            // Adiciona patio
            var patio = await _repository.Adicionar(new PatioEntity
            {
                UrlImgPlanta = entidade.UrlImgPlanta,
            });

            if (patio is null)
                throw new Exception("Erro ao adicionar pátio.");

            var enderecoEntity = entidade.Endereco.ToEntity();
            enderecoEntity.PatioId = patio.Id;

            // Caso contrario adiciona o login e endereco
            var endereco = await _enderecoRepository.Adicionar(enderecoEntity);

            if (endereco is null)
                throw new Exception("Erro ao adicionar endereço.");

            return patio.ToResponse();
        }

        public async Task<PatioResponse?> EditarPatio(int id, PatioRequest entidade)
        {
            var resposta = await _repository.ObterPorId(id);

            if (resposta == null)
                throw new Exception("Pátio não encontrado!");

            // Busca o endereço já existente vinculado ao pátio
            var enderecoExistente = await _enderecoRepository.ObterPorId(resposta.Id);

            if (enderecoExistente == null)
                throw new Exception("Endereço não encontrado para este pátio.");

            // Atualiza os campos
            enderecoExistente.Cep = entidade.Endereco.Cep;
            enderecoExistente.Logradouro = entidade.Endereco.Logradouro;
            enderecoExistente.Numero = entidade.Endereco.Numero;
            enderecoExistente.Bairro = entidade.Endereco.Bairro;
            enderecoExistente.Cidade = entidade.Endereco.Cidade;
            enderecoExistente.Estado = entidade.Endereco.Estado;

            var endereco = await _enderecoRepository.Atualizar(enderecoExistente);

            if (endereco is not null)
            {
                var patioEntity = await _repository.ObterEntityPorId(id);
                if (patioEntity == null)
                    throw new Exception("Pátio não encontrado para atualizar.");

                patioEntity.UrlImgPlanta = entidade.UrlImgPlanta;
                patioEntity.Endereco = endereco;

                var model = await _repository.Atualizar(patioEntity);

                if (model is null)
                    throw new Exception("Erro ao atualizar pátio.");

                return model.ToResponse();
            }

            return null;
        }


        public async Task<PatioResponse?> ObterPatioPorId(int id)
        {
            var resposta = await _repository.ObterPorId(id);
            if (resposta is not null)
            {
                return resposta;
            }
            return null;
        }

        public async Task<IEnumerable<PatioResponse>> ObterTodosPatios()
        {
            var patios = await _repository.ObterTodos();
            if (patios == null) return [];

            return patios;
        }

        public async Task<PatioEntity?> DeletarPatio(int id)
        {
            var patio = await _repository.ObterEntityPorId(id);
            
            if (patio != null)
            {
                var login = await _loginRepository.ObterPorId(patio.Login.Id);

                if (login is not null)
                {
                    _enderecoRepository.Deletar(patio.Endereco);
                    _loginRepository.Deletar(login);

                    _repository.Deletar(patio);
                }
            }
            return patio;
        }
    }
}
