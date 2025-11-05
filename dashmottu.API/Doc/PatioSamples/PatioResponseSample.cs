using dashmottu.API.Application.DTOs;
using dashmottu.API.Domain.Entities;
using Swashbuckle.AspNetCore.Filters;

namespace dashmottu.API.Doc.PatioSamples
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
