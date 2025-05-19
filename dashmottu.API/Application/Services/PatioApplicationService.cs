using dashmottu.API.Application.Interfaces;
using dashmottu.API.Domain.DTOs;
using dashmottu.API.Domain.Entities;
using dashmottu.API.Domain.Interfaces;

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


        public PatioCreateDto? AdicionarPatio(PatioCreateDto patioDto)
        {
            var login = new LoginEntity { Usuario = patioDto.Login.Usuario, Senha = patioDto.Login.Senha };
            var endereco = new EnderecoEntity { 
                Cep = patioDto.Endereco.Cep, 
                Cidade = patioDto.Endereco.Cidade, 
                Estado = patioDto.Endereco.Estado, 
                Logradouro = patioDto.Endereco.Logradouro, 
                Numero = patioDto.Endereco.Numero, 
                Bairro = patioDto.Endereco.Bairro 
            };

            var loginAdicionado = _loginRepository.Adicionar(login);
            var enderecoAdicionado = _enderecoRepository.Adicionar(endereco);
        
            if (loginAdicionado is not null || enderecoAdicionado is not null)
            { 
                var patio = new PatioEntity { 
                    IdEndereco = enderecoAdicionado.Id, 
                    IdLogin = loginAdicionado.Id, 
                    UrlImgPlanta = patioDto.UrlImgPlanta 
                };
                var obj = _repository.Adicionar(patio);
                return new PatioCreateDto { Id = obj.Id, Endereco = patioDto.Endereco, Login = patioDto.Login, UrlImgPlanta = obj.UrlImgPlanta};
            }
            return null;
        }
    }
}
