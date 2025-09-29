using dashmottu.API.Application.DTOs;
using dashmottu.API.Domain.DTOs;
using Swashbuckle.AspNetCore.Filters;

namespace dashmottu.API.Doc.Samples
{
    public class MotoResponseListSample : IExamplesProvider<IEnumerable<MotoResponse>>
    {
        public IEnumerable<MotoResponse> GetExamples()
        {
            return new List<MotoResponse>
            {
                new MotoResponse(
                    1,
                    "TAG123456",
                    "MOOT_POP",
                    "ABC-1234",
                    "PENDENCIA"
                ),
                new MotoResponse(
                    2,
                    "TAG654321",
                    "MOTO_SPORT",
                    "XYZ-5678",
                    "REGULAR"
                )
            };
        }
    }
}
