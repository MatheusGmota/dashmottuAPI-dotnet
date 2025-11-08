using dashmottu.API.Application.DTOs;
using dashmottu.API.Application.Mappers;
using dashmottu.API.Application.Services;
using dashmottu.API.Domain.Entities;
using dashmottu.API.Domain.Interfaces;
using Moq;
using Xunit;

namespace dashmottu.API.Tests.APP
{
    public class PatioApplicationServiceTest
    {
        private readonly Mock<IPatioRepository> _repository;
        private readonly Mock<IEnderecoRepository> _enderecoRepository;
        private readonly PatioApplicationService _service;

        public PatioApplicationServiceTest()
        {
            _repository = new Mock<IPatioRepository>();
            _enderecoRepository = new Mock<IEnderecoRepository>();
            _service = new PatioApplicationService(_repository.Object, _enderecoRepository.Object);
        }

        [Fact]
        [Trait("ApplicationService", "Patios")]
        public async Task ObterTodosPatios_DeveRetonarTodosOsPatios()
        {
            //Arrange  
            var listaPatios = new PageResultModel<IEnumerable<PatioResponse?>>
            {
                Data = new List<PatioResponse>
                {
                    new PatioResponse(
                        1,
                        "http://url1.png",
                        new EnderecoDto
                        (
                            "12345-678",
                            "Rua A",
                            100,
                            "Bairro X",
                            "Cidade Y",
                            "Estado Z"
                        )
                    ),
                    new PatioResponse(
                        2,
                        "http://url2.png",
                        new EnderecoDto
                        (
                            "87654-321",
                            "Rua B",
                            200,
                            "Bairro Y",
                            "Cidade Z",
                            "Estado W"
                        )
                    )
                },
                Deslocamento = 0,
                Limite = 3,
                Total = 2,
            };

            _repository.Setup(obj => obj.ObterTodos(0, 3)).Returns(Task.FromResult(listaPatios));

            //Act  
            var resultado = await _service.ObterTodosPatios(0, 3);

            //Assert  
            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Value.Data.ToList().Count);
        }

        [Fact]
        [Trait("ApplicationService", "Patio")]
        public async Task AdicionarAsync_DeveRetonarPatio()
        {
            // Arrange  
            var patioRequest = new PatioRequest(
                "https://http.cat/images/800.jpg",
                new EnderecoDto(
                    "12345-678",
                    "Rua Exemplo",
                    110,
                    "Centro",
                    "São Paulo",
                    "SP"
                )
            );

            var patioEntity = patioRequest.ToEntity();
            var enderecoEntity = patioRequest.Endereco.ToEntity();

            _repository
                .Setup(r => r.Adicionar(It.IsAny<PatioEntity>()))
                .ReturnsAsync(patioEntity);

            _enderecoRepository
                .Setup(e => e.Adicionar(It.IsAny<int>(), It.IsAny<EnderecoEntity>()))
                .ReturnsAsync(enderecoEntity);

            // Act  
            var resultado = await _service.AdicionarPatio(patioRequest);

            // Assert  
            Assert.NotNull(resultado);
            Assert.Equal(patioRequest.UrlImgPlanta, resultado.Value!.UrlImgPlanta);
        }


        [Fact]
        [Trait("ApplicationService", "Patio")]
        public async Task ObterPorId_DeveRetonarPatio()
        {
            //Arrange  
            var patioResponse = new PatioResponse(
                1,
                "http://url2.png",
                new EnderecoDto
                    (
                        "87654-321",
                        "Rua B",
                        200,
                        "Bairro Y",
                        "Cidade Z",
                        "Estado W"
                    ));

            _repository.Setup(obj => obj.ObterPorId(It.IsAny<int>())).Returns(Task.FromResult(patioResponse)!);
            //Act  
            var resultado = await _service.ObterPatioPorId(1);
            //Assert  
            Assert.NotNull(resultado);
            Assert.Equal(patioResponse.UrlImgPlanta, resultado.Value!.UrlImgPlanta);
        }

        [Fact]
        [Trait("ApplicationService", "Patio")]
        public async Task DeletarPatio_DeveRetornarPatioDeletado()
        {
            //Arrange  
            var patioEntity = new PatioEntity
            {
                Id = 1,
                UrlImgPlanta = "http://url2.png",
                Endereco = new EnderecoEntity
                {
                    Cep = "87654-321",
                    Logradouro = "Rua B",
                    Numero = 200,
                    Bairro = "Bairro Y",
                    Cidade = "Cidade Z",
                    Estado = "Estado W"
                }
            };
            _repository.Setup(obj => obj.Deletar(It.IsAny<int>())).Returns(Task.FromResult(patioEntity)!);
            //Act  
            var resultado = await _service.DeletarPatio(1);
            //Assert  
            Assert.NotNull(resultado);
            Assert.Equal(patioEntity.UrlImgPlanta, resultado.Value!.UrlImgPlanta);
        }
    }
}
