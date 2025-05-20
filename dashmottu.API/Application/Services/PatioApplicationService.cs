using dashmottu.API.Application.Interfaces;
using dashmottu.API.Domain.DTOs;
using dashmottu.API.Domain.Entities;
using dashmottu.API.Domain.Interfaces;
using System.Collections.Generic;

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

        public PatioCreateDto? EditarPatio(int id, PatioCreateDto patioDto)
        {
            var login = new LoginEntity { Usuario = patioDto.Login.Usuario, Senha = patioDto.Login.Senha };
            var endereco = new EnderecoEntity
            {
                Cep = patioDto.Endereco.Cep,
                Cidade = patioDto.Endereco.Cidade,
                Estado = patioDto.Endereco.Estado,
                Logradouro = patioDto.Endereco.Logradouro,
                Numero = patioDto.Endereco.Numero,
                Bairro = patioDto.Endereco.Bairro
            };

            var loginAdicionado = _loginRepository.Atualizar(login);
            var enderecoAdicionado = _enderecoRepository.Atualizar(endereco);

            if (loginAdicionado is not null || enderecoAdicionado is not null)
            {
                var patio = new PatioEntity
                {
                    Id = id,
                    IdEndereco = enderecoAdicionado.Id,
                    IdLogin = loginAdicionado.Id,
                    UrlImgPlanta = patioDto.UrlImgPlanta
                };
                var obj = _repository.Atualizar(patio);
                return new PatioCreateDto { Id = obj.Id, Endereco = patioDto.Endereco, Login = patioDto.Login, UrlImgPlanta = obj.UrlImgPlanta };
            }
            return null;
        }

        public PatioCreateDto? ObterPatioPorId(int id)
        {
            var porId = _repository.ObterPorId(id);
            var login = _loginRepository.ObterPorId(porId.IdLogin);
            var endereco = _enderecoRepository.ObterPorId(porId.IdEndereco);
            return new PatioCreateDto
            {
                Id = porId.Id,
                UrlImgPlanta = porId.UrlImgPlanta,
                Login = new LoginDto
                {
                    Usuario = login.Usuario,
                    Senha = login.Senha
                },
                Endereco = new EnderecoDto
                {
                    Cep = endereco.Cep,
                    Cidade = endereco.Cidade,
                    Estado = endereco.Estado,
                    Logradouro = endereco.Logradouro,
                    Numero = endereco.Numero,
                    Bairro = endereco.Bairro
                }
            };
        }

        public IEnumerable<PatioCreateDto> ObterTodosPatios()
        {
            return _repository.ObterTodos().Select(item =>
            {
                var login = _loginRepository.ObterPorId(item.IdLogin);
                var endereco = _enderecoRepository.ObterPorId(item.IdEndereco);

                return new PatioCreateDto
                {
                    Id = item.Id,
                    UrlImgPlanta = item.UrlImgPlanta,
                    Login = new LoginDto
                    {
                        Usuario = login.Usuario,
                        Senha = login.Senha
                    },
                    Endereco = new EnderecoDto
                    {
                        Cep = endereco.Cep,
                        Cidade = endereco.Cidade,
                        Estado = endereco.Estado,
                        Logradouro = endereco.Logradouro,
                        Numero = endereco.Numero,
                        Bairro = endereco.Bairro
                    }
                };
            }).ToList();
        }
    }
}
