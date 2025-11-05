using dashmottu.API.Application.DTOs;
using Swashbuckle.AspNetCore.Filters;

namespace dashmottu.API.Doc.LoginSamples
{
    public class LoginRequestSample : IExamplesProvider<LoginDto>
    {
        public LoginDto GetExamples()
        {
            return new LoginDto("admin", "admin123");
        }
    }
}