using Swashbuckle.AspNetCore.Filters;

namespace dashmottu.API.Doc.Samples
{
    public class PatioResponseListSample : IExamplesProvider<IEnumerable<object>>
    {
        public IEnumerable<object> GetExamples()
        {
            return new List<object>
            {
                new
                {
                    Id = 1,
                    Endereco = new
                    {
                        Id = 1,
                        Cep = "12345-678",
                        Logradouro = "Rua Exemplo",
                        Numero = "100",
                        Bairro = "Bairro Exemplo",
                        Cidade = "Cidade Exemplo",
                        Estado = "EX"
                    },
                    Login = new
                    {
                        Id = 1,
                        Usuario = "usuario_exemplo"
                    },
                    UrlImgPlanta = "http://exemplo.com/imagem.png"
                },
                new
                {
                    Id = 2,
                    Endereco = new
                    {
                        Id = 2,
                        Cep = "87654-321",
                        Logradouro = "Avenida Teste",
                        Numero = "200",
                        Bairro = "Bairro Teste",
                        Cidade = "Cidade Teste",
                        Estado = "TS"
                    },
                    Login = new
                    {
                        Id = 2,
                        Usuario = "teste_usuario"
                    },
                    UrlImgPlanta = "http://teste.com/imagem.png"
                }
            };
        }
    }
}
