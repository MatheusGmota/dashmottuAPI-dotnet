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
            var usuarioExistente = ObterTodosPatios().FirstOrDefault(p => p.Login.Usuario.Equals(patioDto.Login.Usuario));
            if (usuarioExistente != null)
                throw new Exception("Já existe um cadastro com este nome de usuário.");

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

            var loginAdicionado = _loginRepository.Adicionar(login);
            var enderecoAdicionado = _enderecoRepository.Adicionar(endereco);

            if (loginAdicionado is not null || enderecoAdicionado is not null)
            {
                var patio = new PatioEntity
                {
                    IdEndereco = enderecoAdicionado.Id,
                    IdLogin = loginAdicionado.Id,
                    UrlImgPlanta = patioDto.UrlImgPlanta
                };
                var obj = _repository.Adicionar(patio);
                return new PatioCreateDto { Id = obj.Id, Endereco = patioDto.Endereco, Login = patioDto.Login, UrlImgPlanta = obj.UrlImgPlanta };
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
            if (porId is not null)
            {
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
            return null;
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

        public PatioEntity? DeletarPatio(int id)
        {
            var patio = _repository.ObterPorId(id);
            if (patio != null)
            {
                var endereco = _enderecoRepository.ObterPorId(patio.IdEndereco);
                var login = _loginRepository.ObterPorId(patio.IdLogin);
                if (endereco is not null && login is not null)
                {
                    _enderecoRepository.Deletar(endereco);
                    _loginRepository.Deletar(login);

                    _repository.Deletar(patio);
                }
            }
            return patio;
        }

        public LoginResponseDto ValidarLogin(LoginDto login)
        {
            var patio = ObterTodosPatios().FirstOrDefault(p => 
                p.Login.Usuario.Equals(login.Usuario) && 
                p.Login.Senha.Equals(login.Senha));

            return new LoginResponseDto
            {
                IsValid = patio != null,
                IdPatio = patio?.Id,
                Token = patio != null ? GenerateToken(patio) : null
            };
        }

        private string GenerateToken(PatioCreateDto patio)
        {
            // Gera um token aleatório baseado no ID do pátio + data + um GUID
            var rawToken = $"{patio.Id}-{DateTime.UtcNow.Ticks}-{Guid.NewGuid()}";

            // Codifica em Base64 
            var tokenBytes = System.Text.Encoding.UTF8.GetBytes(rawToken);
            return Convert.ToBase64String(tokenBytes);
        }

    }
}
