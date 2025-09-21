using dashmottu.API.Application.DTOs;
using dashmottu.API.Domain.DTOs;
using Swashbuckle.AspNetCore.Filters;

namespace dashmottu.API.Doc.Samples
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
