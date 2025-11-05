using dashmottu.API.Application.DTOs;
using Swashbuckle.AspNetCore.Filters;

namespace dashmottu.API.Doc.PatioSamples
{
    public class PatioResponseListSample : IExamplesProvider<IEnumerable<PatioResponse>>
    {
        public IEnumerable<PatioResponse> GetExamples()
        {
            return new List<PatioResponse>
            {
                new PatioResponse(
                    1,
                    "http://exemplo.com/imagem.png",
                    new EnderecoDto
                    (
                        "12345-678",
                        "Rua Exemplo",
                        100,
                        "Bairro Exemplo",
                        "Cidade Exemplo",
                        "EX"
                    )
                ),
                new PatioResponse(
                    2,
                    "http://teste.com/imagem.png",
                    new EnderecoDto
                    (
                        "87654-321",
                        "Avenida Teste",
                        200,
                        "Bairro Teste",
                        "Cidade Teste",
                        "TS"
                    )
                )
            };
        }
    }
}
