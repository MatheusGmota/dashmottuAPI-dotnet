using dashmottu.API.Application.DTOs;
using Swashbuckle.AspNetCore.Filters;

namespace dashmottu.API.Doc.LoginSamples
{
    public class LoginRequestCreateSample : IExamplesProvider<LoginRequestDto>
    {
        public LoginRequestDto GetExamples()
        {
            return new LoginRequestDto("admin", "admin123", "Admin");
        }
    }
}
