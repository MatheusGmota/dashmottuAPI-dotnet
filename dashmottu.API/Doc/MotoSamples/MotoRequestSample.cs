using dashmottu.API.Application.DTOs;
using Swashbuckle.AspNetCore.Filters;

namespace dashmottu.API.Doc.Samples
{
    public class MotoRequestSample : IExamplesProvider<MotoRequest>
    {

        public MotoRequest GetExamples()
        {
            return new MotoRequest("TAG12345", "MOTO_SPORT", "ABC1234", "PENDENCIA");
        }
    }
}
