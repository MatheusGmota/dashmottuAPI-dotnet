using dashmottu.API.Domain.Entities;
using dashmottu.API.Application.DTOs;
using dashmottu.API.Application.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Moq;
using System.Net;
using System.Security.Claims;
using System.Text.Encodings.Web;
using Xunit;

namespace dashmottu.API.Tests.APP
{
    public class TestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public const string Scheme = "TestAuth";

        public TestAuthHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock) : base(options, logger, encoder, clock) { }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, "tester"),
                new Claim(ClaimTypes.Role, "Admin"),
            };

            var identity = new ClaimsIdentity(claims, Scheme);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme);

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }

    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        public Mock<IPatioApplicationService> IPatioApplicationServiceMock { get; } = new();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Cria o Mock do IClienteUseCase
                services.RemoveAll(typeof(IPatioApplicationService));
                services.AddSingleton(IPatioApplicationServiceMock.Object);

                // Autenticação fake
                services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = TestAuthHandler.Scheme;
                    options.DefaultChallengeScheme = TestAuthHandler.Scheme;
                })
                .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(TestAuthHandler.Scheme, _ => { });
            });
        }
    }


    public class PatioControllerTest: IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;

        public PatioControllerTest(CustomWebApplicationFactory factory)
        {
            _factory = factory;
        }


        [Fact(DisplayName = "ObterTodos - Retornar todos os dados do patio")]
        [Trait("Controller", "Patio")]
        public async Task ObterTodos_DeveVoltarValores()
        {
            // Arrange
            var retornoPatio = new PageResultModel<IEnumerable<PatioResponse>>
            {
                Data = new List<PatioResponse>
                {
                    new PatioResponse
                    (
                        1,
                        "http://example.com/planta1.jpg",
                        new EnderecoDto
                        (
                            "12345-678",
                            "Rua A",
                            1,
                            "Bairro X",
                            "Cidade Y",
                            "Estado Z"
                        )
                    )
                },
                Deslocamento = 0,
                Limite = 1,
                Total = 1
            };

            var retorno = OperationResult<PageResultModel<IEnumerable<PatioResponse>>>.Success(retornoPatio, 200);

            _factory.IPatioApplicationServiceMock
                .Setup(x => x.ObterTodosPatios(0, 3))
                .ReturnsAsync(retorno);

            using var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/patio");


            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        }
    }
}
