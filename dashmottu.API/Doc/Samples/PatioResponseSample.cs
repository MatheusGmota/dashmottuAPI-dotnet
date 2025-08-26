using dashmottu.API.Domain.DTOs;
using dashmottu.API.Domain.Entities;
using Swashbuckle.AspNetCore.Filters;

namespace dashmottu.API.Doc.Samples
{
    public class PatioResponseSample : IExamplesProvider<PatioCreateDto>
    {
        public PatioCreateDto GetExamples()
        {
            return new PatioCreateDto
            {
                Id = 1,
                Endereco = new EnderecoDto
                {
                    Cep = "12345-678",
                    Cidade = "São Paulo",
                    Estado = "SP",
                    Logradouro = "Rua Exemplo",
                    Numero = 110,
                    Bairro = "Centro"
                },
                Login = new LoginDto
                {
                    Usuario = "Patio1",
                    Senha = "senha123"
                },
                UrlImgPlanta = "https://http.cat/images/800.jpg"
            };
        }
    }
}
