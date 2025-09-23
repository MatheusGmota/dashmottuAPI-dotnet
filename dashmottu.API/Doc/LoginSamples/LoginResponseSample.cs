using dashmottu.API.Domain.DTOs;
using Swashbuckle.AspNetCore.Filters;

namespace dashmottu.API.Doc.Samples
{
    public class LoginResponseSample : IExamplesProvider<LoginResponseDto>
    {
        public LoginResponseDto GetExamples()
        {
            return new LoginResponseDto(
                new LoginDto("admin", "admin123"),
                true,
                1,
                "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9eyJzdWIiOiIxIiwibmFtZSI6ImFkbWluIiwiaWF0IjoxNTE2MjM5MDIyfQ"
            );
        }
    }
}
