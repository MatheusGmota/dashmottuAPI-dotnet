using dashmottu.API.Domain.DTOs;
using Swashbuckle.AspNetCore.Filters;

namespace dashmottu.API.Doc.Samples
{
    public class PatioRequestSample : IExamplesProvider<PatioCreateDto>
    {

        public PatioCreateDto GetExamples()
        {
            return new PatioCreateDto
            {
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
