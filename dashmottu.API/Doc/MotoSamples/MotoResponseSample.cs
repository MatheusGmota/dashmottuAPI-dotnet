using dashmottu.API.Application.DTOs;
using Swashbuckle.AspNetCore.Filters;

namespace dashmottu.API.Doc.Samples
{
    public class MotoResponseSample : IExamplesProvider<MotoResponse>
    {
        public MotoResponse GetExamples()
        {
            return new MotoResponse(
                1,
                "TAG123456",
                "MOOT_POP",
                "ABC-1234",
                "PENDENCIA"
            );
        }
    }
}
