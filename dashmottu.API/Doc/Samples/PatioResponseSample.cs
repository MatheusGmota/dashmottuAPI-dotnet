using dashmottu.API.Domain.DTOs;
using dashmottu.API.Domain.Entities;
using Swashbuckle.AspNetCore.Filters;

namespace dashmottu.API.Doc.Samples
{
    public class PatioResponseSample : IExamplesProvider<PatioResponse>
    {
        public PatioResponse GetExamples()
        {
            return new PatioResponse(
                1,
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
