using dashmottu.API.Application.DTOs;
using Swashbuckle.AspNetCore.Filters;

namespace dashmottu.API.Doc.PatioSamples
{
    public class PatioRequestSample : IExamplesProvider<PatioRequest>
    {

        public PatioRequest GetExamples()
        {
            return new PatioRequest(
                    "https://http.cat/images/800.jpg",
                    new EnderecoDto
                    (
                        "12345-678",
                        "Rua Exemplo",
                        110,
                        "Centro",
                        "São Paulo",
                        "SP"
                    )
            );
        }
    }
}
